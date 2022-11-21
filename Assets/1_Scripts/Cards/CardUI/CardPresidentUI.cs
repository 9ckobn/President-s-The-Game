using Cards.Data;
using Cards.View;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(CardPresidentView))]
    public class CardPresidentUI : CardUI
    {
        public CardPresidentData GetData { get => data as CardPresidentData; }

        public CardPresidentData SetCardData
        {
            set
            {
                data = value;

                (view as CardPresidentView).SetData(data as CardPresidentData);

                cardImage.sprite = data.Sprite;
            }
        }

        protected override void SelectCard()
        {
            UIManager.GetWindow<DeckBuildWindow>().SelectPresidentCard(this);
        }

        protected override void DeSelectCard()
        {
            UIManager.GetWindow<DeckBuildWindow>().DeSelectPresidentCard(this);
        }
    }
}