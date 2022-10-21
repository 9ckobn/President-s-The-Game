using Core;
using Gameplay;
using UnityEngine;

namespace Cards
{
    public class CardPresidentData : CardDataBase
    {
        private const int BUFF_CLIMATE = 2, DEBUFF_CLIMATE = -1;

        public string Name { get; private set; }
        public int Rarityrank { get; private set; }
        public int Attack { get; private set; }
        public int Defend { get; private set; }
        public int Luck { get; private set; }
        public int Diplomatic { get; private set; }

        public int BuffAttack { get; private set; }
        public int BuffDefend { get; private set; }
        public int BuffLuck { get; private set; }
        public int BuffDiplomatic { get; private set; }

        public TypeClimate Climate { get; private set; }

        public CardPresidentData(CardPresidentDataSerialize data, Sprite sprite) : base(data.id.ToString(), sprite)
        {
            Name = data.name;
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
                BuffAttack += BUFF_CLIMATE;
                BuffDefend += BUFF_CLIMATE;
                BuffLuck += BUFF_CLIMATE;
                BuffDiplomatic += BUFF_CLIMATE;
            }
            else
            {
                BuffAttack = DEBUFF_CLIMATE;
                BuffDefend = DEBUFF_CLIMATE;
                BuffLuck = DEBUFF_CLIMATE;
                BuffDiplomatic = DEBUFF_CLIMATE;
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