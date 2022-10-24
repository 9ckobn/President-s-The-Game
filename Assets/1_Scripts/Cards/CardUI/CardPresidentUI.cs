using Cards.Data;
using Cards.View;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(CardPresidentView))]
    [RequireComponent(typeof(Button))]
    public class CardPresidentUI : CardUI
    {
        public CardPresidentData GetData { get => data as CardPresidentData; }

        public CardPresidentData SetCardData
        {
            set
            {
                data = value;

                (view as CardPresidentView).SetData(data as CardPresidentData);
            }
        }

        protected override void SelectCard()
        {
            UIManager.Instance.GetWindow<DeckBuildWindow>().SelectPresidentCard(this);
        }

        protected override void DeSelectCard()
        {
            UIManager.Instance.GetWindow<DeckBuildWindow>().DeSelectPresidentCard(this);
        }
    }
}