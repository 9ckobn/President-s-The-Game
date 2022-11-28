using Cards;
using Cards.Data;
using Core;
using Data;
using DG.Tweening;
using EffectSystem;
using Gameplay;
using System.Collections.Generic;
using System.Linq;
using UI.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SelectBuffAttributeWindow : Window
    {
        public UnityEvent OnClickStartGame;

        [SerializeField] private BuffAttributeCardPresidentUI presidentCardPrefab;

        [SerializeField] private SelectedAttribute[] attributesObjects;
        [SerializeField] private Sprite foodIcon, medicineIcon, economicIcon, rawMaterialsIcon;
        [SerializeField] private BuffCardsPresidentParent cardsParent;
        [SerializeField] private GameObject spawnParent, parentSelectedCard;
        [SerializeField] private Button startGameButton;

        private List<BuffAttributeCardPresidentUI> cards = new List<BuffAttributeCardPresidentUI>();

        private Sequence startButtonSequence;

        private Tween myTween;

        protected override void AfterInitialization()
        {
            if (attributesObjects.Length != 4)
            {
                LogManager.LogError("Count SelectedAttributes != 4");
            }
            else
            {
                attributesObjects[0].Init(foodIcon, TypeAttribute.Food);
                attributesObjects[1].Init(medicineIcon, TypeAttribute.Medicine);
                attributesObjects[2].Init(economicIcon, TypeAttribute.Economic);
                attributesObjects[3].Init(rawMaterialsIcon, TypeAttribute.RawMaterials);
            }

            startGameButton.onClick.AddListener(() =>
            {
                HideButtonStartGame();
                Hide();
                OnClickStartGame?.Invoke();
            });
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
                bool blockAttribute = true;

                foreach (var buff in dataBuff)
                {
                    if (attribute.TypeAttribute == buff.TypeAttribute)
                    {
                        blockAttribute = false;
                        attribute.ShowInfo(buff.Value);
                    }
                }

                if (blockAttribute)
                {
                    attribute.ShowBlock();
                }
            }
        }

        public void HideDataAttributes()
        {
            foreach (var attribute in attributesObjects)
            {
                attribute.HideInfo();
            }
        }

        public void ShowButtonStartGame()
        {
            if (!startGameButton.gameObject.activeSelf) {
                startGameButton.gameObject.SetActive(true);

                myTween = startGameButton.gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1.5f).SetLoops(-1, LoopType.Yoyo);
            } 
        }

        public void HideButtonStartGame()
        {
            startGameButton.gameObject.SetActive(false);
            myTween.Kill();
            startGameButton.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}