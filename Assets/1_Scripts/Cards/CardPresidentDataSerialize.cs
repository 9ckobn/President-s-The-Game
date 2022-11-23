using System;

namespace Cards
{
    [Serializable]
    public class CardPresidentDataSerialize
    {
        public int id;
        public string name;
        public int rarityrank;
        public int attack;
        public int defend;
        public int luck;
        public int diplomatic;
        public bool economy;
        public bool health_care;
        public bool raw_materials;
        public bool food;
        public string climate_zone;
    }
}