using Cards.Data;
using Cards.View;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardPresidentView))]
    public class CardPresidentModel : CardModel
    {
        public CardPresidentData SetCardData 
        {
            set
            { 
                data = value;

                (view as CardPresidentView).SetData(data as CardPresidentData);
            }
        }

        protected override void PointerEnter()
        {
            throw new System.NotImplementedException();
        }

        protected override void PointerExit()
        {
            throw new System.NotImplementedException();
        }
    }
}