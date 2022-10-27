using Cards;
using Cards.Data;
using Core;
using NaughtyAttributes;
using SceneObjects;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CreatorController", menuName = "Controllers/Gameplay/CreatorController")]
    public class CreatorController : BaseController
    {
        [BoxGroup("Prefabs")]
        [SerializeField] private CardPresidentModel presidentPrefab;
        [BoxGroup("Prefabs")]
        [SerializeField] private CardFightModel fightPrefab;

        [BoxGroup("Models")]
        [SerializeField] private GameObject[] modelPresident, modelsFights;

        public CardPresidentModel CreateCardPresident(CardPresidentData cardData)
        {
            Transform spawnTransform = ObjectsOnScene.Instance.GetSpawnPosition.transform;
            GameObject modelPrefab = BoxController.GetController<StorageCardsController>().GetPresidentModel(cardData.Id);

            CardPresidentModel card = Instantiate(presidentPrefab, spawnTransform);
            card.SetCardData = cardData;
            GameObject model = Instantiate(modelPrefab, spawnTransform);
            card.SetMode = model;

            return card;
        }

        public CardFightModel CreateCardFight(CardFightData cardData)
        {
            Transform spawnTransform = ObjectsOnScene.Instance.GetSpawnPosition.transform;
            GameObject modelPrefab = BoxController.GetController<StorageCardsController>().GetFightModel(cardData.Id);

            CardFightModel card = Instantiate(fightPrefab, spawnTransform);
            card.SetCardData = cardData;
            GameObject model = Instantiate(modelPrefab, spawnTransform);
            card.SetMode = model;

            return card;
        }
    }
}