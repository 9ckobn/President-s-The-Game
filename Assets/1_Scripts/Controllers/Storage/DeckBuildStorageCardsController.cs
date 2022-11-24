using Cards.Data;
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.DeckBuild
{
    [CreateAssetMenu(fileName = "DeckBuildStorageCardsController", menuName = "Controllers/DeckBuild/DeckBuildStorageCardsController")]
    public class DeckBuildStorageCardsController : StorageCardsController
    {
        public override void AfterInitialize()
        {
            List<CardPresidentDataSerialize> serializePresidents = DataBaseManager.Instance.CardsPresidentsData;

            foreach (var president in serializePresidents)
            {
                Sprite sprite = storageImages.GetPresidentSprite(president.id.ToString());
                CardPresidentData cardData = new CardPresidentData(president, sprite);

                CardsPresidentData.Add(cardData);
            }

            foreach (var card in cardFightSCRO)
            {
                CardFightData cardData = new CardFightData(card, card.Sprite, card.Effects);

                CardsFightData.Add(cardData);
            }
        }

        public CardPresidentData GetPresidentData(string id)
        {
            foreach (var cardData in CardsPresidentData)
            {
                if (cardData.Id == id)
                {
                    return cardData;
                }
            }

            LogManager.LogError($"Not have president card with id - {id}");

            return null;
        }

        public CardFightData GetFightData(string id)
        {
            foreach (var cardData in CardsFightData)
            {
                if (cardData.Id == id)
                {
                    return cardData;
                }
            }

            LogManager.LogError($"Not have fight card with id - {id}");

            return null;
        }
    }
}