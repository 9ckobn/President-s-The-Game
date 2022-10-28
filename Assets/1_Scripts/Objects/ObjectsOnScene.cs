using Cards.Container;
using NaughtyAttributes;
using UnityEngine;

namespace SceneObjects
{
    public class ObjectsOnScene : Singleton<ObjectsOnScene>
    {
        [BoxGroup("Spwan position")]
        [SerializeField] private GameObject spawnPosition;
        [BoxGroup("Containers")]
        [SerializeField] private ContainerFightCards containerFights;
        [BoxGroup("Containers")]
        [SerializeField] private ContainerPresidentCards playerPresidents, enemyPresidents;
        [BoxGroup("Containers")]
        [SerializeField] private ArrowTargetController arrowTarget;

        public GameObject GetSpawnPosition { get => spawnPosition; }
        public ContainerFightCards GetContainerFights { get => containerFights; }
        public ContainerPresidentCards GetContainerPlayerPresidents { get => playerPresidents; }
        public ContainerPresidentCards GetContainerEnemyPresidents { get => enemyPresidents; }
        public ArrowTargetController GetArrowTarget { get => arrowTarget; }
    }
}