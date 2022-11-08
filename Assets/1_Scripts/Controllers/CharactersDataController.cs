using Cards;
using Cards.Data;
using Core;
using Data;
using EffectSystem;
using SceneObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CharactersDataController", menuName = "Controllers/Gameplay/CharactersDataController")]
    public class CharactersDataController : BaseController
    {
        private const int DEFAULT_VALUE = 10;

        private CharacterData playerData, enemyData;

        public CharacterData GetPlayerData { get => playerData; }
        public CharacterData GetEnemyData { get => enemyData; }

        public override void OnInitialize()
        {
            playerData = new CharacterData(CreateAttributes(), ObjectsOnScene.Instance.GetBuildingsStorage.GetPlayerBuildings, true);
            enemyData = new CharacterData(CreateAttributes(), ObjectsOnScene.Instance.GetBuildingsStorage.GetEnemyBuildings, false);
        }

        private List<AttributeData> CreateAttributes()
        {
            List<AttributeData> attributes = new List<AttributeData>();
            List<CardPresidentData> playersPresidents = BoxController.GetController<StorageCardsController>().GetCardsPresidentData;

            int attack = DEFAULT_VALUE;
            int defend = DEFAULT_VALUE;
            int luck = DEFAULT_VALUE;
            int diplomatic = DEFAULT_VALUE;

            foreach (var president in playersPresidents)
            {
                attack = president.Attack + president.BuffAttack.GetValue;
                defend = president.Defend + president.BuffDefend.GetValue;
                luck = president.Luck + president.BuffLuck.GetValue;
                diplomatic = president.Diplomatic + president.BuffDiplomatic.GetValue;
            }

            int economic = attack / 2 + diplomatic / 2;
            int[] statesEconomic = new int[3] { economic - economic / 4, economic / 3, 0 };

            int food = defend / 2 + luck / 2;
            int[] statesFood = new int[3] { food - food / 4, food / 3, 0 };

            int rawMaterials = attack / 2 + luck / 2;
            int[] statesRawMaterials = new int[3] { rawMaterials - rawMaterials / 4, rawMaterials / 3, 0 };

            int medicine = defend / 2 + diplomatic / 2;
            int[] statesMedicine = new int[3] { medicine - medicine / 4, medicine / 3, 0 };

            int morality = economic + food + rawMaterials + medicine;

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
    }
}