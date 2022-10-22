using Cards.Data;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardFightData))]
    [RequireComponent(typeof(CardFightView))]
    public class CardFight : CardBase
    {
        public CardFightData SetCardData { set => data = value; }
    }
}