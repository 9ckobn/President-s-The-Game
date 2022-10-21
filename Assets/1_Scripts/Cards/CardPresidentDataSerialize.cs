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
        public string climate_zone;
    }
}