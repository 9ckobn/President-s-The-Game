using Cards;
using Cards.Container;
using Cards.Data;
using Core;
using Data;
using SceneObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CardsController", menuName = "Controllers/Gameplay/CardsController")]
    public class CardsController : BaseController
    {
        private List<CardPresidentModel> presidentCards = new List<CardPresidentModel>();
        private List<CardFightModel> fightCards = new List<CardFightModel>();

        public override void OnStart()
        {
            List<CardPresidentData> cardsPresidentData = BoxController.GetController<StorageCardsController>().GetCardsPresidentData;
            List<CardFightData> GetCardsFightData = BoxController.GetController<StorageCardsController>().GetCardsFightData;

            CreatorController creator = BoxController.GetController<CreatorController>();
            ContainerPresidentCards containerPlayerPresidents = ObjectsOnScene.Instance.GetContainerPlayerPresidents;
            ContainerFightCards containerFightCards = ObjectsOnScene.Instance.GetContainerFights;

            foreach (var cardData in cardsPresidentData)
            {
                CardPresidentModel card = creator.CreateCardPresident(cardData);
                presidentCards.Add(card);
            }

            foreach (var cardData in GetCardsFightData)
            {
                CardFightModel card = creator.CreateCardFight(cardData);
                fightCards.Add(card);
            }

            for (int i = 0; i < presidentCards.Count && i < MainData.MAX_PRESIDENT_CARDS; i++)
            {
                containerPlayerPresidents.AddCard(presidentCards[i]);
            }

            for (int i = 0; i < fightCards.Count && i < MainData.MAX_FIGHT_CARDS; i++)
            {
                containerFightCards.AddCard(fightCards[i]);
            }

            containerFightCards.SetMaxCards = MainData.MAX_FIGHT_CARDS;
            containerPlayerPresidents.SetMaxCards = MainData.MAX_PRESIDENT_CARDS;
        }
    }
}