using Cards.Data;
using Core;
using Gameplay;
using System.Collections.Generic;
using System.Linq;
using Tutorial;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "GameStorageCardsController", menuName = "Controllers/Gameplay/GameStorageCardsController")]
    public class GameStorageCardsController : StorageCardsController
    {
        public List<CardPresidentData> CardsEnemyPresidentData { get; private set; }
        public List<CardFightData> CardsEnemyFightData { get; private set; }

        public override void AfterInitialize()
        {
            CardsEnemyPresidentData = new List<CardPresidentData>();
            CardsEnemyFightData = new List<CardFightData>();

            List<string> idPresidents = new List<string>(), idEnemyPresidents = new List<string>();
            List<string> idFightsCards = new List<string>(), idEnemyFightsCards = new List<string>();

            List<CardPresidentDataSerialize> serializePresidents = DataBaseManager.Instance.CardsPresidentsData;

            if (BoxController.GetController<FightSceneController>().IsTutorNow)
            {
                SCRO_TutorialData tutorData = BoxController.GetController<TutorialController>().GetTutorialData;

                foreach (var president in tutorData.PlayerPresidentCards)
                {
                    idPresidents.Add(president.ToString());
                }

                foreach (var president in tutorData.EnemyPresidentCards)
                {
                    idEnemyPresidents.Add(president.ToString());
                }

                foreach (var fight in tutorData.PlayerFightCards)
                {
                    idFightsCards.Add(fight.Id);
                }

                foreach (var fight in tutorData.EnemyFightCards)
                {
                    idEnemyFightsCards.Add(fight.Id);
                }
            }
            else
            {
                idPresidents = DataBaseManager.Instance.SelectedDeck.PresidentsId;
                idFightsCards = DataBaseManager.Instance.SelectedDeck.FightsId;
            }


            foreach (var idPresident in idPresidents)
            {
                Sprite sprite = storageImages.GetPresidentSprite(idPresident);
                CardPresidentDataSerialize serializeData = serializePresidents.First(c => c.id.ToString() == idPresident);
                CardPresidentData cardData = new CardPresidentData(serializeData, sprite);

                CardsPresidentData.Add(cardData);
            }

            foreach (var idPresident in idEnemyPresidents)
            {
                Sprite sprite = storageImages.GetPresidentSprite(idPresident);
                CardPresidentDataSerialize serializeData = serializePresidents.First(c => c.id.ToString() == idPresident);
                CardPresidentData cardData = new CardPresidentData(serializeData, sprite);

                CardsEnemyPresidentData.Add(cardData);
            }


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

            foreach (var id in idEnemyFightsCards)
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

            LogManager.LogError($"Not have president model with id {id} in storageImages");
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

            LogManager.LogError($"Not have fight model with id {id} in storageImages");
            return null;
        }
    }
}