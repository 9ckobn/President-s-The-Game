using UnityEngine;

namespace Cards.Data
{
    public class CardFightData : CardDataBase
    {
        public string Description { get; private set; }
        public int Cost { get; private set; }

        public CardFightData(string id) : base(id, null, "fight card") { } // DELETE

        public CardFightData(SCRO_CardFight data, Sprite sprite) : base(data.ID.ToString(), sprite, data.name)
        {
            Description = data.Description;
            Cost = data.Cost;
        }
    }
}