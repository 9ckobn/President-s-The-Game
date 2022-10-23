using UnityEngine;

namespace Cards.Data
{
    public class CardFightData : CardDataBase
    {
        public int Cost { get; private set; }

        public CardFightData(string id) : base(id, null) { } // DELETE

        public CardFightData(SCRO_CardFight data, Sprite sprite) : base(data.ID.ToString(), sprite)
        {

        }
    }
}