using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardPresidentData))]
    [RequireComponent(typeof(CardPresidentView))]
    public class CardPresident : CardBase
    {
        public CardPresidentData SetCardData 
        {
            set
            { 
                data = value;

                (view as CardPresidentView).SetData(data as CardPresidentData, data.Sprite);
            }
        }
    }
}