using Cards.Data;
using Cards.View;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardFightView))]
    public class CardFight : CardBase
    {
        public CardFightData SetCardData 
        {
            set
            {
                data = value;

                (view as CardFightView).SetData(data as CardFightData);
            }
        }
    }
}