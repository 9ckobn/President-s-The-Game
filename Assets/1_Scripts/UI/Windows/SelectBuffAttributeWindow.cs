using Cards;
using Cards.Data;
using Core;
using Data;
using EffectSystem;
using System.Collections.Generic;
using UI.Components;
using UnityEngine;

namespace UI
{
    public class SelectBuffAttributeWindow : Window
    {
        [SerializeField] private BuffAttributeCardPresidentUI presidentCardPrefab;

        [SerializeField] private SelectedAttribute[] attributesObjects;
        [SerializeField] private Sprite foodIcon, medicineIcon, economicIcon, rawMaterialsIcon;
        [SerializeField] private BuffCardsPresidentParent cardsParent;
        [SerializeField] private GameObject spawnParent, parentSelectedCard;

        private List<BuffAttributeCardPresidentUI> cards = new List<BuffAttributeCardPresidentUI>();

        protected override void AfterInitialization()
        {
            if(attributesObjects.Length != 4)
            {
                BoxController.GetController<LogController>().LogError("Count SelectedAttributes != 4");
            }
            else
            {
                attributesObjects[0].Init(foodIcon, TypeAttribute.Food);
                attributesObjects[1].Init(medicineIcon, TypeAttribute.Medicine);
                attributesObjects[2].Init(economicIcon, TypeAttribute.Economic);
                attributesObjects[3].Init(rawMaterialsIcon, TypeAttribute.RawMaterials);
            }
        }

        protected override void BeforeShow()
        {
            List<CardPresidentData> cardsData = BoxController.GetController<GameStorageCardsController>().CardsPresidentData;

            foreach (var cardData in cardsData)
            {
                BuffAttributeCardPresidentUI card = Instantiate(presidentCardPrefab, spawnParent.transform);

                card.SetCardData = cardData;
                cards.Add(card);
            }

            cardsParent.AddCardsInStart(cards);
        }

        public void PutCardInSelectedParent(BuffAttributeCardPresidentUI card)
        {
            card.transform.SetParent(parentSelectedCard.transform);
            cardsParent.RemoveCard(card);
        }

        public void PutCardInCardsParent(BuffAttributeCardPresidentUI card)
        {
            card.Attribute = null;
            cardsParent.AddCard(card);
            card.ReturnStartScale();
        }

        public void EnableRaycastCards()
        {
            foreach (var card in cards)
            {
                card.EnableRaycat();
            }
        }

        public void DisableRaycastCards()
        {
            foreach (var card in cards)
            {
                card.DisableRaycat();
            }
        }

        public void ShowDataAttributes(List<DataBuffPresidents> dataBuff)
        {
            foreach (var attribute in attributesObjects)
            {
                attribute.ShowInfo(2);
            }
        }

        public void HideDataAttributes()
        {
            foreach (var attribute in attributesObjects)
            {
                attribute.HideInfo();
            }
        }
    }
}