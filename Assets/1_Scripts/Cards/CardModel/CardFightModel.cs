using Cards.Data;
using Cards.View;
using Core;
using Gameplay;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardFightView))]
    public class CardFightModel : CardModel
    {
        public CardFightData SetCardData 
        {
            set
            {
                data = value;

                (view as CardFightView).SetData(data as CardFightData);
            }
        }

        protected override void PointerEnter()
        {
            BoxController.GetController<CardsController>().SelectFightCard(this);
        }

        protected override void PointerExit()
        {
        }
    }
}