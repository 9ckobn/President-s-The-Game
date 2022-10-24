using Cards.Data;
using Cards.Storage;
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "StorageCardsController", menuName = "Controllers/Gameplay/StorageCardsController")]
    public class StorageCardsController : BaseController
    {
        создать остальные карты и сделать фейковою загрузку для deckBuildScene
        [SerializeField] private StorageCardImages storageImages;
        [SerializeField] private SCRO_CardFight[] cardFightSCRO;

        private List<CardPresidentData> cardsPresidentData = new List<CardPresidentData>();
        private List<CardFightData> cardsFightData = new List<CardFightData>();

        public List<CardPresidentData> GetCardsPresidentData { get => cardsPresidentData; }
        public List<CardFightData> GetCardsFightData { get => cardsFightData; }

        public override void OnInitialize()
        {
            List<CardPresidentDataSerialize> cardList = DataBaseManager.Instance.GetCardsPresidentData;

            foreach (var card in cardList)
            {
                Sprite sprite = storageImages.GetPresidentSprite(card.id.ToString());
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