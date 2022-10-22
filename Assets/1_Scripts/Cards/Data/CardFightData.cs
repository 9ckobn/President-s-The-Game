using UnityEngine;

namespace Cards.Data
{
    public class CardFightData : CardDataBase
    {
        public int Cost { get; private set; }

        public CardFightData(CardFightSCRO data, Sprite sprite) : base(data.ID.ToString(), sprite)
        {

        }
    }
}