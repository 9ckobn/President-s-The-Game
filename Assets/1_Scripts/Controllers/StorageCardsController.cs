using Cards.Storage;
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "StorageCardsController", menuName = "Controllers/Gameplay/StorageCardsController")]
    public class StorageCardsController : BaseController
    {
        [SerializeField] private StorageCardImages storageImages;
        [SerializeField] private CardFightSCRO[] cardFightSCRO;

        private List<CardPresidentData> cardsPresidentData = new List<CardPresidentData>();
        private List<CardFightData> cardsFightData = new List<CardFightData>();

        public List<CardPresidentData> GetCardsPresidentData { get => cardsPresidentData; }
        public List<CardFightData> GetCardsFightData { get => cardsFightData; }

        public override void OnInitialize()
        {
            CardsPresidentsList cardList = DataBaseManager.Instance.GetCardsPresident;

            foreach (var card in cardList.player)
            {
                Sprite sprite = storageImages.GetPresidentSprite(card.id);
                CardPresidentData cardData = new CardPresidentData(card, sprite);

                cardsPresidentData.Add(cardData);
            }

            foreach (var card in cardFightSCRO)
            {
                Sprite image = storageImages.GetFightSprite(card.ID);
                CardFightData cardData = new CardFightData(card, image);

                cardsFightData.Add(cardData);
            }
        }
    }
}