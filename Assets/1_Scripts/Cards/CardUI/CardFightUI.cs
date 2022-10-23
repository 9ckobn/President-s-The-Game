using Cards.Data;
using Cards.View;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(CardFightData))]
    [RequireComponent(typeof(CardFightView))]
    public class CardFightUI : CardBase
    {
        [SerializeField] private Image cardImage;

        public CardFightData SetCardData { set => data = value; }
    }
}