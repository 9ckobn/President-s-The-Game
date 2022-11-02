using Buildings;
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
        [SerializeField] private ContainerFightCards containerFights, containerAIFightCards;
        [BoxGroup("Containers")]
        [SerializeField] private ContainerPresidentCards playerPresidents, enemyPresidents;
        [BoxGroup("Containers")]
        [SerializeField] private ArrowTargetController arrowTarget;
        [BoxGroup("Building storate")]
        [SerializeField] private BuildingsStorage buildingsStorage;

        public GameObject GetSpawnPosition { get => spawnPosition; }
        public ContainerFightCards GetContainerPlayerFights { get => containerFights; }
        public ContainerFightCards GetContainerAIFightCards { get => containerAIFightCards; }
        public ContainerPresidentCards GetContainerPlayerPresidents { get => playerPresidents; }
        public ContainerPresidentCards GetContainerEnemyPresidents { get => enemyPresidents; }
        public ArrowTargetController GetArrowTarget { get => arrowTarget; }
        public BuildingsStorage GetBuildingsStorage { get => buildingsStorage; }
    }
}