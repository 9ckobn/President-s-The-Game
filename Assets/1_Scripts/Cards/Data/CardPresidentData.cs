using Cards.Type;
using Core;
using Gameplay;
using UnityEngine;

namespace Cards.Data
{
    public class CardPresidentData : CardDataBase
    {
        private const int BUFF_CLIMATE = 2, DEBUFF_CLIMATE = -1;

        public int Rarityrank { get; private set; }
        public int Attack { get; private set; }
        public int Defend { get; private set; }
        public int Luck { get; private set; }
        public int Diplomatic { get; private set; }

        public BuffAttribute BuffAttack { get; private set; }
        public BuffAttribute BuffDefend { get; private set; }
        public BuffAttribute BuffLuck { get; private set; }
        public BuffAttribute BuffDiplomatic { get; private set; }

        public TypeClimate Climate { get; private set; }

        public int CommonAttack { get => Attack + BuffAttack.GetValue; }
        public int CommonDefend { get => Defend + BuffDefend.GetValue; }
        public int CommonLuck { get => Luck + BuffLuck.GetValue; }
        public int CommonDiplomatic { get => Diplomatic + BuffDiplomatic.GetValue; }

        //
        // DELETE
        //
        public CardPresidentData(string id) : base(id, null, "president card")
        {
            Rarityrank = 1;
            Attack = 5;
            Defend = 5;
            Luck = 5;
            Diplomatic = 5;

            DefineClimate("temperate");
            CalculateClimate();
        }

        public CardPresidentData(CardPresidentDataSerialize data, Sprite sprite) : base(data.id.ToString(), sprite, data.name)
        {
            Rarityrank = data.rarityrank;
            Attack = data.attack;
            Defend = data.defend;
            Luck = data.luck;
            Diplomatic = data.diplomatic;

            DefineClimate(data.climate_zone);
            CalculateClimate();
        }

        private void CalculateClimate()
        {
            if (Climate == BoxController.GetController<DeckBuildController>().GetTypeClimate)
            {
                BuffAttack = new BuffAttribute(BUFF_CLIMATE);
                BuffDefend = new BuffAttribute(BUFF_CLIMATE);
                BuffLuck = new BuffAttribute(BUFF_CLIMATE);
                BuffDiplomatic = new BuffAttribute(BUFF_CLIMATE);
            }
            else
            {
                BuffAttack = new BuffAttribute(DEBUFF_CLIMATE);
                BuffDefend = new BuffAttribute(DEBUFF_CLIMATE);
                BuffLuck = new BuffAttribute(DEBUFF_CLIMATE);
                BuffDiplomatic = new BuffAttribute(DEBUFF_CLIMATE);
            }
        }

        private void DefineClimate(string climate)
        {
            if (climate == "temperate")
            {
                Climate = TypeClimate.Temperate;
            }
            else if (climate == "equatorial")
            {
                Climate = TypeClimate.Equatorial;
            }
            else if (climate == "tropical")
            {
                Climate = TypeClimate.Tropical;
            }
        }
    }
}