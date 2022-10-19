using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardPresidentData))]
    [RequireComponent(typeof(CardPresidentView))]
    public class CardPresident : CardBase
    {
        public CardPresidentData SetCardData { set => data = value; }
        public CardPresidentView SetCardView { set => view = value; }
    }
}