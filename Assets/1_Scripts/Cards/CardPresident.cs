using Cards.Data;
using Cards.View;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardPresidentView))]
    public class CardPresident : CardBase
    {
        public CardPresidentData SetCardData 
        {
            set
            { 
                data = value;

                (view as CardPresidentView).SetData(data as CardPresidentData);
            }
        }

        protected override void AfterAwake()
        {
            throw new System.NotImplementedException();
        }
    }
}