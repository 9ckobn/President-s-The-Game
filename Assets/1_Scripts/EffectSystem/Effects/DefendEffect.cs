using EffectSystem.SCRO;

namespace EffectSystem
{
    public class DefendEffect : Effect
    {
        public TypeAttribute[] TypeDefends { get; private set; } // Какой объект защищён
        public int ValueDefend { get; private set; } // Сколько % защиты получает

        public DefendEffect(SCRO_DefendEffect data) : base(data)
        {
            TypeDefends = data.TypeDefends;
            ValueDefend = data.ValueProtect;
        }
    }
}