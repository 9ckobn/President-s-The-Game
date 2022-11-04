using Cards.Data;
using Cards.View;
using NaughtyAttributes;
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

        protected override void MouseEnter()
        {
            throw new System.NotImplementedException();
        }

        protected override void MouseExit()
        {
            throw new System.NotImplementedException();
        }

        protected override void UseCard()
        {
        }

        protected override void StopUseCard()
        {
        }
    }
}