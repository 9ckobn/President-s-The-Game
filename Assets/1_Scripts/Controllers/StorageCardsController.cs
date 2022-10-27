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

            List<string> idFightsCards = DataBaseManager.Instance.GetCardsFightID;

            foreach (var id in idFightsCards)
            {
                foreach (var card in cardFightSCRO)
                {
                    if (card.Id == id)
                    {
                        CardFightData cardData = new CardFightData(card, card.Sprite);

                        cardsFightData.Add(cardData);
                    }
                }
            }
        }

        public CardPresidentData GetPresidentData(string id)
        {
            foreach (var cardData in cardsPresidentData)
            {
                if(cardData.Id == id)
                {
                    return cardData;
                }
            }

            BoxController.GetController<LogController>().LogError($"Not have president card with id - {id}");

            return null;
        }

        public CardFightData GetFightData(string id)
        {
            foreach (var cardData in cardsFightData)
            {
                if (cardData.Id == id)
                {
                    return cardData;
                }
            }

            BoxController.GetController<LogController>().LogError($"Not have fight card with id - {id}");

            return null;
        }

        public GameObject GetPresidentModel(string id)
        {
            foreach (var imageData in storageImages.PresidentImages)
            {
                if(imageData.ID == id)
                {
                    return imageData.Model;
                }
            }

            BoxController.GetController<LogController>().LogError($"Not have president model with id {id} in storageImages");
            return null;
        }

        public GameObject GetFightModel(string id)
        {
            foreach (var cardFight in cardFightSCRO)
            {
                if (cardFight.Id == id)
                {
                    return cardFight.Model;
                }
            }

            BoxController.GetController<LogController>().LogError($"Not have fight model with id {id} in storageImages");
            return null;
        }
    }
}