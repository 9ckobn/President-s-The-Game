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
        private List<CardFightModel> enemyFightCards = new List<CardFightModel>();

        private CardFightModel selectedFightCard;

        public override void OnStart()
        {
            List<CardPresidentData> cardsPresidentData = BoxController.GetController<StorageCardsController>().GetCardsPresidentData;
            List<CardFightData> cardsFightData = BoxController.GetController<StorageCardsController>().GetCardsFightData;

            CreatorController creator = BoxController.GetController<CreatorController>();
            ContainerPresidentCards containerPlayerPresidents = ObjectsOnScene.Instance.GetContainerPlayerPresidents;
            ContainerFightCards containerFightCards = ObjectsOnScene.Instance.GetContainerPlayerFights;

            containerFightCards.SetMaxCards = MainData.MAX_FIGHT_CARDS;
            containerPlayerPresidents.SetMaxCards = MainData.MAX_PRESIDENT_CARDS;

            foreach (var cardData in cardsPresidentData)
            {
                CardPresidentModel card = creator.CreateCardPresident(cardData);
                playerPresidentCards.Add(card);
            }

            foreach (var cardData in cardsFightData)
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

            // Create enemy AI cards

            // TODO: not use player data presidents data!!! Use enemy persidents data!!!

            List<CardPresidentData> enemyCardsPresidentData = new List<CardPresidentData>(cardsPresidentData);
            List<CardFightData> cardsEnemyFightData = BoxController.GetController<StorageCardsController>().GetCardsEnemyFightData;

            ContainerPresidentCards containerEnemyPresidentCards = ObjectsOnScene.Instance.GetContainerEnemyPresidents;
            ContainerFightCards containerEnemyFightCards = ObjectsOnScene.Instance.GetContainerAIFightCards;

            containerEnemyFightCards.SetMaxCards = MainData.MAX_FIGHT_CARDS;
            containerEnemyPresidentCards.SetMaxCards = MainData.MAX_PRESIDENT_CARDS;

            foreach (var cardData in enemyCardsPresidentData)
            {
                CardPresidentModel card = creator.CreateCardPresident(cardData);
                enemyPresidentCards.Add(card);
            }

            foreach (var cardData in cardsEnemyFightData)
            {
                CardFightModel card = creator.CreateCardFight(cardData);
                enemyFightCards.Add(card);
            }

            for (int i = 0; i < enemyPresidentCards.Count && i < MainData.MAX_PRESIDENT_CARDS; i++)
            {
                containerEnemyPresidentCards.AddCard(enemyPresidentCards[i]);
            }

            for (int i = 0; i < enemyFightCards.Count && i < MainData.MAX_FIGHT_CARDS; i++)
            {
                containerEnemyFightCards.AddCard(enemyFightCards[i]);
            }
        }

        public void ShowCardsCharacter(bool isPlayer)
        {
            ObjectsOnScene.Instance.GetContainerPlayerFights.gameObject.SetActive(isPlayer);
            ObjectsOnScene.Instance.GetContainerAIFightCards.gameObject.SetActive(!isPlayer);

            foreach (var card in playerPresidentCards)
            {
                card.ChangeHighlight(isPlayer);
            }

            foreach (var card in enemyPresidentCards)
            {
                card.ChangeHighlight(!isPlayer);
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

            selectedFightCard = null;

            BoxController.GetController<FightSceneController>().AddCountUseCards();
        }
    }
}