using Cards.�ontainer;
using NaughtyAttributes;
using UnityEngine;

namespace SceneObjects
{
    public class ObjectsOnScene : Singleton<ObjectsOnScene>
    {
        [BoxGroup("Spwan position")]
        [SerializeField] private GameObject spawnPosition;
        [BoxGroup("Containers")]
        [SerializeField] private �ontainerFightCards containerFights;
        [BoxGroup("Containers")]
        [SerializeField] private �ontainerPresidentCards playerPresidents, enemyPresidents;

        public GameObject GetSpawnPosition { get => spawnPosition; }
        public �ontainerFightCards GetContainerFights { get => containerFights; }
        public �ontainerPresidentCards GetContainerPlayerPresidents { get => playerPresidents; }
        public �ontainerPresidentCards GetContainerEnemyPresidents { get => enemyPresidents; }
    }
}