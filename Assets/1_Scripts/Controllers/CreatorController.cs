using Cards;
using Cards.Data;
using Core;
using SceneObjects;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CreatorController", menuName = "Controllers/Gameplay/CreatorController")]
    public class CreatorController : BaseController
    {
        [SerializeField] private CardPresident presidentPrefab;
        [SerializeField] private CardFight fightPrefab;

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