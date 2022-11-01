using Cards;
using Cards.Container;
using Cards.Data;
using Core;
using Data;
using EffectSystem;
using SceneObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CardsController", menuName = "Controllers/Gameplay/CardsController")]
    public class CardsController : BaseController
    {
        private List<CardPresidentModel> playerPresidentCards = new List<CardPresidentModel>();
        private List<CardPresidentModel> enemyPresidentCards = new List<CardPresidentModel>();

        private List<CardFightModel> playerFightCards = new List<CardFightModel>();
        private List<CardFightModel>  enemyFightCards = new List<CardFightModel>();

        private CardFightModel selectedFightCard;

        public override void OnStart()
        {
            List<CardPresidentData> cardsPresidentData = BoxController.GetController<StorageCardsController>().GetCardsPresidentData;
            List<CardFightData> GetCardsFightData = BoxController.GetController<StorageCardsController>().GetCardsFightData;

            CreatorController creator = BoxController.GetController<CreatorController>();
            ContainerPresidentCards containerPlayerPresidents = ObjectsOnScene.Instance.GetContainerPlayerPresidents;
            ContainerFightCards containerFightCards = ObjectsOnScene.Instance.GetContainerFights;

            containerFightCards.SetMaxCards = MainData.MAX_FIGHT_CARDS;
            containerPlayerPresidents.SetMaxCards = MainData.MAX_PRESIDENT_CARDS;

            foreach (var cardData in cardsPresidentData)
            {
                CardPresidentModel card = creator.CreateCardPresident(cardData);
                playerPresidentCards.Add(card);
            }

            foreach (var cardData in GetCardsFightData)
            {
                CardFightModel card = creator.CreateCardFight(cardData);
                playerFightCards.Add(card);
            }

            for (int i = 0; i < playerPresidentCards.Count && i < MainData.MAX_PRESIDENT_CARDS; i++)
            {
                containerPlayerPresidents.AddCard(playerPresidentCards[i]);
            }

            for (int i = 0; i < playerFightCards.Count && i < MainData.MAX_FIGHT_CARDS; i++)
            {
                containerFightCards.AddCard(playerFightCards[i]);
            }
        }

        public void SelectFightCard(CardFightModel card)
        {
            selectedFightCard = card;
        }

        public void DeselectFightCard(CardFightModel card)
        {
            selectedFightCard = null;
        }

        public void ClickFightCard(CardFightModel card)
        {
            if (card.CheckCanUseCard())
            {
                BoxController.GetController<EffectsController>().ApplyFightCardEffects(card);
            }
        }

        public void EndUseCard(CardFightModel card)
        {
            card.EndUseCard();
        }

        public void BlockAllCards()
        {
            foreach (var card in playerFightCards)
            {
                card.BlockCard();
            }
        }
    }
}