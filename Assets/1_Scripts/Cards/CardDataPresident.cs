using Core;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardDataPresident : CardDataBase
    {
        private const int START_BUFF = 10, BUFF_CLIMATE = 2, DEBUFF_CLIMATE = -1;

        public string Name { get; private set; }
        public string Factor_materials = "";
        public string Factor_economic = "";
        public string Factor_health = "";
        public string Factor_food = "";

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

        public int Materials_ability_protect = 0;
        public int Economic_ability_protect = 0;
        public int Economic_ability_attack = 0;
        public int Health_ability_protect = 0;
        public int Food_ability_protect = 0;
        public int Food_ability_attack = 0;

        public int BUFFmaterials;
        public int BUFFeconomic;
        public int BUFFhealth;
        public int BUFFfood;

        public CardDataPresident(int id, string name, int level, TypeClimate climate,
            int buff_diplomation, int buff_diplomation_delta, int buff_fortune, int buff_fortune_delta, int buff_protection, int buff_protection_delta, int buff_attack, int buff_attack_delta,
            string factor_materials, string factor_economic,
            string factor_health, string factor_food) : base(id)
        {
            Name = name;
            Factor_materials = factor_materials;
            Factor_economic = factor_economic;
            Factor_health = factor_health;
            Factor_food = factor_food;

            Level = level;
            Climate = climate;

            Buff_diplomation = buff_diplomation + START_BUFF;
            Buff_diplomation_delta = buff_diplomation_delta;
            Buff_fortune = buff_fortune + START_BUFF;
            Buff_fortune_delta = buff_fortune_delta;
            Buff_protection = buff_protection + START_BUFF;
            Buff_protection_delta = buff_protection_delta;
            Buff_attack = buff_attack + START_BUFF;
            Buff_attack_delta = buff_attack_delta;

            BUFFmaterials = (Buff_fortune + Buff_attack) / 2;
            BUFFeconomic = (Buff_attack + Buff_diplomation) / 2;
            BUFFhealth = (Buff_protection + Buff_diplomation) / 2;
            BUFFfood = (Buff_protection + Buff_fortune) / 2;

            CalculateClimate();
        }

        private void CalculateClimate()
        {
            if(Climate == BoxController.GetController<DataGameController>().GetTypeClimate)
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
    }
}