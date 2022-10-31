using EffectSystem.SCRO;

namespace EffectSystem
{
    public class DefendEffect : Effect
    {
        public TypeAttribute TypeDefend { get; private set; } // Какой объект защищён
        public bool Immortal { get; private set; } // Совсем не получает урона?
        public int ValueProtect { get; private set; } // Сколько % защиты получает
        public int DurationProtect { get; private set; } // Сколько атак длится защита

        public DefendEffect(SCRO_DefendEffect data) : base(data)
        {
            TypeDefend = data.TypeProtect;
            Immortal = data.Immortal;
            ValueProtect = data.ValueProtect;
            DurationProtect = data.DurationProtect;
        }
    }
}