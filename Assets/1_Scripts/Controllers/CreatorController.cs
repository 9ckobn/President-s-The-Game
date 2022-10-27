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
        [SerializeField] private CardPresident presidentPrefab;
        [BoxGroup("Prefabs")]
        [SerializeField] private CardFight fightPrefab;

        [BoxGroup("Models")]
        [SerializeField] private GameObject[] modelPresident, modelsFights;

        public CardPresident CreateCardPresident(CardPresidentData cardData)
        {
            Transform spawnTransform = ObjectsOnScene.Instance.GetSpawnPosition.transform;

            CardPresident card = Instantiate(presidentPrefab, spawnTransform);
            card.SetCardData = cardData;

            return card;
        }

        public CardFight CreateCardFight(CardFightData cardData)
        {
            Transform spawnTransform = ObjectsOnScene.Instance.GetSpawnPosition.transform;

            CardFight card = Instantiate(fightPrefab, spawnTransform);
            card.SetCardData = cardData;

            return card;
        }
    }
}