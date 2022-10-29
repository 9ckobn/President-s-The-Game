using EffectSystem.SCRO;

namespace EffectSystem
{
    public class ProtectEffect : Effect
    {
        public TypeAttribute TypeProtect { get; private set; } // Какой объект защищён
        public bool Immortal { get; private set; } // Совсем не получает урона?
        public int ValueProtect { get; private set; } // Сколько % защиты получает
        public int DurationProtect { get; private set; } // Сколько атак длится защита

        public ProtectEffect(SCRO_ProtectEffect data) : base(data)
        {
            TypeProtect = data.TypeProtect;
            Immortal = data.Immortal;
            ValueProtect = data.ValueProtect;
            DurationProtect = data.DurationProtect;
        }
    }
}
