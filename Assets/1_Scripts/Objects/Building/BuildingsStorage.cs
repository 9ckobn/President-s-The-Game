using Core;
using EffectSystem;
using UnityEngine;

namespace Buildings
{
    public class BuildingsStorage : MonoBehaviour
    {
        [SerializeField] private Building[] playerBuildings, enemyBuildings;

        public Building[] GetPlayerBuildings { get => playerBuildings; }
        public Building[] GetEnemyBuildings { get => enemyBuildings; }

        public Building GetPlayerBuilding(TypeAttribute type)
        {
            foreach (var building in playerBuildings)
            {
                if(building.GetTypeBuilding == type)
                {
                    return building;
                }
            }

            LogManager.LogError($"Not have player building with type {type}");

            return null;
        }

        public Building GetEnemyBuilding(TypeAttribute type)
        {
            foreach (var building in enemyBuildings)
            {
                if (building.GetTypeBuilding == type)
                {
                    return building;
                }
            }

            LogManager.LogError($"Not have enemy building with type {type}");

            return null;
        }
    }
}