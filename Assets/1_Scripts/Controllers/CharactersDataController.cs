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
            playerData = new CharacterData(ObjectsOnScene.Instance.GetBuildingsStorage.GetPlayerBuildings);
            enemyData = new CharacterData(ObjectsOnScene.Instance.GetBuildingsStorage.GetEnemyBuildings);

            CreateAttributes();
        }

        private void CreateAttributes()
        {
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
            int food = defend / 2 + luck / 2;
            int rawMaterials = attack / 2 + luck / 2;
            int medicine = defend / 2 + diplomatic / 2;
            int morality = economic + food + rawMaterials + medicine;

            playerData.AddAttribute(TypeAttribute.Attack, attack);
            playerData.AddAttribute(TypeAttribute.Defend, defend);
            playerData.AddAttribute(TypeAttribute.Luck, luck);
            playerData.AddAttribute(TypeAttribute.Diplomacy, diplomatic);
            playerData.AddAttribute(TypeAttribute.Economic, economic);
            playerData.AddAttribute(TypeAttribute.Food, food);
            playerData.AddAttribute(TypeAttribute.RawMaterials, rawMaterials);
            playerData.AddAttribute(TypeAttribute.Medicine, medicine);
            playerData.AddAttribute(TypeAttribute.Morality, morality);

            enemyData.AddAttribute(TypeAttribute.Attack, attack);
            enemyData.AddAttribute(TypeAttribute.Defend, defend);
            enemyData.AddAttribute(TypeAttribute.Luck, luck);
            enemyData.AddAttribute(TypeAttribute.Diplomacy, diplomatic);
            enemyData.AddAttribute(TypeAttribute.Economic, economic);
            enemyData.AddAttribute(TypeAttribute.Food, food);
            enemyData.AddAttribute(TypeAttribute.RawMaterials, rawMaterials);
            enemyData.AddAttribute(TypeAttribute.Medicine, medicine);
            enemyData.AddAttribute(TypeAttribute.Morality, morality);
        }
    }
}