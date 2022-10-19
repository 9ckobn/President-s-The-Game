using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "StorageCardsController", menuName = "Controllers/Gameplay/StorageCardsController")]
    public class StorageCardsController : BaseController
    {
        private List<CardPresidentData> cardsPresidentData = new List<CardPresidentData>();

        public List<CardPresidentData> GetCardsPresidentData { get => cardsPresidentData; }

        public override void OnInitialize()
        {
            CardsPresidentsList cardList = DataBaseManager.Instance.GetCardsPresident;

            foreach (var card in cardList.player)
            {
                CardPresidentData cardData = new CardPresidentData(card);

                cardsPresidentData.Add(cardData);
            }
        }
    }
}