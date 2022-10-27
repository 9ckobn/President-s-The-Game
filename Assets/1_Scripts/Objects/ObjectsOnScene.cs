using Cards.Ñontainer;
using NaughtyAttributes;
using UnityEngine;

namespace SceneObjects
{
    public class ObjectsOnScene : Singleton<ObjectsOnScene>
    {
        [BoxGroup("Spwan position")]
        [SerializeField] private GameObject spawnPosition;
        [BoxGroup("Containers")]
        [SerializeField] private ÑontainerFightCards containerFights;
        [BoxGroup("Containers")]
        [SerializeField] private ÑontainerPresidentCards playerPresidents, enemyPresidents;

        public GameObject GetSpawnPosition { get => spawnPosition; }
        public ÑontainerFightCards GetContainerFights { get => containerFights; }
        public ÑontainerPresidentCards GetContainerPlayerPresidents { get => playerPresidents; }
        public ÑontainerPresidentCards GetContainerEnemyPresidents { get => enemyPresidents; }
    }
}