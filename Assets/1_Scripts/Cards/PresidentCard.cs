using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class PresidentCard : CardBase
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public string Climate { get; private set; }
        public int Buff_diplomation { get; private set; }
        public int Buff_diplomation_delta { get; private set; }
        public int Buff_fortune { get; private set; }
        public int Buff_fortune_delta { get; private set; }
        public int Buff_protection { get; private set; }
        public int Buff_protection_delta { get; private set; }
        public int Buff_attack { get; private set; }
        private int BUFF_Initial = 10; // Первоначально все атрибуты равны 10 
        public int Buff_attack_delta = 0;

        public string Factor_materials = "";
        public int Materials_ability_protect = 0;
        public string Factor_economic = "";
        public int Economic_ability_protect = 0;
        public int Economic_ability_attack = 0;
        public string Factor_health = "";
        public int Health_ability_protect = 0;
        public string Factor_food = "";
        public int Food_ability_protect = 0;
        public int Food_ability_attack = 0;

        public int BUFFmaterials;
        public int BUFFeconomic;
        public int BUFFhealth;
        public int BUFFfood;

        public PresidentCard(int id) : base(id)
        {

        }
    }
}