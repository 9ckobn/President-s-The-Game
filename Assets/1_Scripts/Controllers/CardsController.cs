using Cards;
using Cards.Data;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CardsController", menuName = "Controllers/Gameplay/CardsController")]
    public class CardsController : BaseController
    {
        private List<CardPresidentModel> presidentCards;
        private List<CardFightModel> fightCards;

        public override void OnStart()
        {
            List<CardPresidentData> cardsPresidentData = BoxController.GetController<StorageCardsController>().GetCardsPresidentData;
            List<CardFightData> GetCardsFightData = BoxController.GetController<StorageCardsController>().GetCardsFightData;

            foreach (var cardData in cardsPresidentData)
            {

            }
        }
    }
}