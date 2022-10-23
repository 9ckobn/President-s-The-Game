using Cards.Data;
using Cards.View;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(CardPresidentData))]
    [RequireComponent(typeof(CardPresidentView))]
    public class CardPresidentUI : CardBase
    {
        [SerializeField] private Image cardImage;

        public CardPresidentData SetCardData
        {
            set
            {
                data = value;

                (view as CardPresidentView).SetData(data as CardPresidentData);
            }
        }
    }
}