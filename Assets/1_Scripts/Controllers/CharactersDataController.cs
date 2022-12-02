using Cards;
using Cards.Data;
using Core;
using Data;
using EffectSystem;
using SceneObjects;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CharactersDataController", menuName = "Controllers/Gameplay/CharactersDataController")]
    public class CharactersDataController : BaseController
    {
        private const int DEFAULT_VALUE = 10;

        private bool isPlayerNow = true;
        private CharacterData currentCharacter;
        private CharacterData playerData, enemyData;

        public bool GetIsPlayerNow { get => isPlayerNow; }
        public CharacterData GetCurrentCharacter { get => currentCharacter; }
        public CharacterData GetPlayerData { get => playerData; }
        public CharacterData GetEnemyData { get => enemyData; }

        #region INITIALIZE

        public void CreateCharactersData()
        {
            playerData = new CharacterData(CreateAttributes(), ObjectsOnScene.Instance.GetBuildingsStorage.GetPlayerBuildings, true);
            enemyData = new CharacterData(CreateAttributes(BoxController.GetController<FightSceneController>().IsTutorNow), ObjectsOnScene.Instance.GetBuildingsStorage.GetEnemyBuildings, false);

            if (isPlayerNow)
            {
                currentCharacter = playerData;
            }
            else
            {
                currentCharacter = enemyData;
            }
        }

        private List<AttributeData> CreateAttributes(bool isTutorEnemy = false)
        {
            List<AttributeData> attributes = new List<AttributeData>();
            List<CardPresidentData> playersPresidents = BoxController.GetController<GameStorageCardsController>().CardsPresidentData;

            int attack = isTutorEnemy ? 0 : DEFAULT_VALUE;
            int defend = isTutorEnemy ? 0 : DEFAULT_VALUE;
            int luck = isTutorEnemy ? 0 : DEFAULT_VALUE;
            int diplomatic = isTutorEnemy ? 0 : DEFAULT_VALUE;

            foreach (var president in playersPresidents)
            {
                attack += president.Attack;
                defend += president.Defend;
                luck += president.Luck;
                diplomatic += president.Diplomatic;
            }

            BuffAttributePresidentController buffController = BoxController.GetController<BuffAttributePresidentController>();

            int economic = (attack + diplomatic) * MainData.MULTIPLIER_BUILDING + buffController.GetBuffValue(TypeAttribute.Economic);
            int[] statesEconomic = new int[3] { economic - economic / 4, economic / 3, 0 };

            int food = (defend + luck) * MainData.MULTIPLIER_BUILDING + buffController.GetBuffValue(TypeAttribute.Food);
            int[] statesFood = new int[3] { food - food / 4, food / 3, 0 };

            int rawMaterials = (attack + luck) * MainData.MULTIPLIER_BUILDING + buffController.GetBuffValue(TypeAttribute.RawMaterials);
            int[] statesRawMaterials = new int[3] { rawMaterials - rawMaterials / 4, rawMaterials / 3, 0 };

            int medicine = (defend + diplomatic) * MainData.MULTIPLIER_BUILDING + buffController.GetBuffValue(TypeAttribute.Medicine);
            int[] statesMedicine = new int[3] { medicine - medicine / 4, medicine / 3, 0 };

            int morality = 100;//economic + food + rawMaterials + medicine;

            attributes.Add(new AttributeData(TypeAttribute.Attack, attack));
            attributes.Add(new AttributeData(TypeAttribute.Defend, defend));
            attributes.Add(new AttributeData(TypeAttribute.Luck, luck));
            attributes.Add(new AttributeData(TypeAttribute.Diplomacy, diplomatic));
            attributes.Add(new AttributeData(TypeAttribute.Economic, economic, statesEconomic));
            attributes.Add(new AttributeData(TypeAttribute.Food, food, statesFood));
            attributes.Add(new AttributeData(TypeAttribute.RawMaterials, rawMaterials, statesRawMaterials));
            attributes.Add(new AttributeData(TypeAttribute.Medicine, medicine, statesMedicine));
            attributes.Add(new AttributeData(TypeAttribute.Morality, morality));

            return attributes;
        }

        #endregion

        public void ChangeCurrentCharacter()
        {
            if (isPlayerNow)
            {
                currentCharacter = enemyData;
                UIManager.GetWindow<UIWindow>().SetCurrentCharacterText("Enemy now");
            }
            else
            {
                currentCharacter = playerData;
                UIManager.GetWindow<UIWindow>().SetCurrentCharacterText("Player now");
            }

            isPlayerNow = !isPlayerNow;
        }
    }
}