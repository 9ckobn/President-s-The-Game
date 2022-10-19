using System;
using UnityEngine;

namespace Cards
{
    [Serializable]
    public class CardPresidentDataSerialize : MonoBehaviour
    {
        public string id;
        public string name;
        public int level;
        public string climate;
        public int buff_diplomation;
        public int buff_fortune;
        public int buff_protection;
        public int buff_attack;
        public string factor_materials;
        public int materials_ability_protect;
        public string factor_economic;
        public int economic_ability_protect;
        public int economic_ability_attack;
        public string factor_health;
        public int health_ability_protect;
        public string factor_food;
        public int food_ability_protect;
        public int food_ability_attack;
    }

    [System.Serializable]
    public class CardsPresidentsList
    {
        public CardPresidentDataSerialize[] player;
    }
}