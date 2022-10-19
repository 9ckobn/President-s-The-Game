using Core;
using Gameplay;

namespace Cards
{
    public class CardPresidentData : CardDataBase
    {
        private const int START_BUFF = 10, BUFF_CLIMATE = 2, DEBUFF_CLIMATE = -1;

        public string Name { get; private set; }
        public string Factor_materials { get; private set; }
        public string Factor_economic { get; private set; }
        public string Factor_health { get; private set; }
        public string Factor_food { get; private set; }

        public int Level { get; private set; }
        public TypeClimate Climate { get; private set; }

        public int Buff_diplomation { get; private set; }
        public int Buff_diplomation_delta { get; private set; }
        public int Buff_fortune { get; private set; }
        public int Buff_fortune_delta { get; private set; }
        public int Buff_protection { get; private set; }
        public int Buff_protection_delta { get; private set; }
        public int Buff_attack { get; private set; }
        public int Buff_attack_delta { get; private set; }

        public int Materials_ability_protect { get; private set; }
        public int Economic_ability_protect { get; private set; }
        public int Economic_ability_attack { get; private set; }
        public int Health_ability_protect { get; private set; }
        public int Food_ability_protect { get; private set; }
        public int Food_ability_attack { get; private set; }

        public int BUFFmaterials { get; private set; }
        public int BUFFeconomic { get; private set; }
        public int BUFFhealth { get; private set; }
        public int BUFFfood { get; private set; }

        public CardPresidentData(CardPresidentDataSerialize data) : base(data.id)
        {
            Name = name;
            Factor_materials = data.factor_materials;
            Factor_economic = data.factor_economic;
            Factor_health = data.factor_health;
            Factor_food = data.factor_food;

            Level = data.level;
            DefineClimate(data.climate);

            Buff_diplomation = data.buff_diplomation + START_BUFF;
            //Buff_diplomation_delta = data.buff_diplomation_delta;
            Buff_fortune = data.buff_fortune + START_BUFF;
            //Buff_fortune_delta = data.buff_fortune_delta;
            Buff_protection = data.buff_protection + START_BUFF;
            //Buff_protection_delta = buff_protection_delta;
            Buff_attack = data.buff_attack + START_BUFF;
            //Buff_attack_delta = buff_attack_delta;

            BUFFmaterials = (Buff_fortune + Buff_attack) / 2;
            BUFFeconomic = (Buff_attack + Buff_diplomation) / 2;
            BUFFhealth = (Buff_protection + Buff_diplomation) / 2;
            BUFFfood = (Buff_protection + Buff_fortune) / 2;

            CalculateClimate();
        }

        private void CalculateClimate()
        {
            if (Climate == BoxController.GetController<DeckBuildController>().GetTypeClimate)
            {
                Buff_attack_delta += BUFF_CLIMATE; // Климат совпал
                Buff_diplomation_delta += BUFF_CLIMATE;
                Buff_fortune_delta += BUFF_CLIMATE;
                Buff_protection_delta += BUFF_CLIMATE;
            }
            else
            {
                Buff_attack_delta = Buff_attack_delta - DEBUFF_CLIMATE;
                Buff_diplomation_delta = Buff_diplomation_delta - DEBUFF_CLIMATE;
                Buff_fortune_delta = Buff_fortune_delta - DEBUFF_CLIMATE;
                Buff_protection_delta = Buff_protection_delta - DEBUFF_CLIMATE;
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