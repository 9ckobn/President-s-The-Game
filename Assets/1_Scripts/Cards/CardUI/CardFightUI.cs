using Cards.Data;
using Cards.View;
using UI;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardFightView))]
    public class CardFightUI : CardUI
    {
        public CardFightData GetData { get => data as CardFightData; }

        public CardFightData SetCardData { set => data = value; }

        protected override void SelectCard()
        {
            UIManager.Instance.GetWindow<DeckBuildWindow>().SelectFightCard(this);
        }

        protected override void DeSelectCard()
        {
            UIManager.Instance.GetWindow<DeckBuildWindow>().DeSelectFightCard(this);
        }
    }
}