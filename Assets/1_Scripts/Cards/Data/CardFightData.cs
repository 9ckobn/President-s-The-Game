using UnityEngine;

namespace Cards
{
    public class CardFightData : CardDataBase
    {
        public int Cost { get; private set; }

        public CardFightData(CardFightSCRO data, Sprite sprite) : base(data.ID.ToString(), sprite)
        {

        }
    }
}