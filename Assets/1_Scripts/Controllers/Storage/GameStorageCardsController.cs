using Cards.Data;
using Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "GameStorageCardsController", menuName = "Controllers/Gameplay/GameStorageCardsController")]
    public class GameStorageCardsController : StorageCardsController
    {
        public List<CardFightData> CardsEnemyFightData { get; private set; }

        public override void AfterInitialize()
        {
            CardsEnemyFightData = new List<CardFightData>();

            List<string> idPresidents = DataBaseManager.Instance.SelectedDeck.PresidentsId;
            List<CardPresidentDataSerialize> serializePresidents = DataBaseManager.Instance.CardsPresidentsData;

            foreach (var idPresident in idPresidents)
            {
                Sprite sprite = storageImages.GetPresidentSprite(idPresident);
                CardPresidentDataSerialize serializeData = serializePresidents.First(c => c.id.ToString() == idPresident);
                CardPresidentData cardData = new CardPresidentData(serializeData, sprite);

                CardsPresidentData.Add(cardData);
            }

            List<string> idFightsCards = DataBaseManager.Instance.SelectedDeck.FightsId;

            foreach (var id in idFightsCards)
            {
                foreach (var card in cardFightSCRO)
                {
                    if (card.Id == id)
                    {
                        CardFightData cardData = new CardFightData(card, card.Sprite, card.Effects);

                        CardsFightData.Add(cardData);
                    }
                }
            }

            foreach (var id in idFightsCards)
            {
                foreach (var card in cardFightSCRO)
                {
                    if (card.Id == id)
                    {
                        CardFightData cardData = new CardFightData(card, card.Sprite, card.Effects);

                        CardsEnemyFightData.Add(cardData);
                    }
                }
            }
        }

        public GameObject GetPresidentModel(string id)
        {
            foreach (var imageData in storageImages.PresidentImages)
            {
                if (imageData.ID == id)
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