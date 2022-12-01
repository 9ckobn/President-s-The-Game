using EffectSystem;
using System.Collections.Generic;

namespace Data
{
    public static class MainData
    {
        public const int MAX_PRESIDENT_CARDS = 3;
        public const int MAX_FIGHT_CARDS = 9;
        public const int MAX_DECKS = 3;
        public const int MAX_SIGH_IN_NAME_DECK = 10;
        public const int MAC_LENGTH_DECK_NAME = 10;
        public const float MULTIPLIER_BUFF = 0.5f;
        public const int MULTIPLIER_BUILDING = 4;

        public static TypeAttribute[] TYPE_BUILDINGS = new TypeAttribute[4]
        { TypeAttribute.Medicine, TypeAttribute.Economic, TypeAttribute.Food, TypeAttribute.RawMaterials };
    }
}