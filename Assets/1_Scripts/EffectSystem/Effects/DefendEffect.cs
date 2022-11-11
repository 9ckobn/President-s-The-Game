using EffectSystem.SCRO;

namespace EffectSystem
{
    public class DefendEffect : Effect
    {
        public TypeAttribute[] TypeDefends { get; private set; } // Какой объект защищён
        public bool IsGodDefend { get; private set; }
        public int BaseValue { get; private set; } // Сколько % защиты получает
        public TypeAttribute TypeNeedAttribute { get; private set; }
        public int ValueAttribute { get; private set; }
        public TypeAttribute SelectedPlayerBuilding;

        public DefendEffect(SCRO_DefendEffect data) : base(data)
        {
            TypeDefends = data.TypeDefends;
            IsGodDefend = data.IsGodDefend;
            BaseValue = data.BaseValue;
            TypeNeedAttribute = data.TypeNeedAttribute;
            ValueAttribute = data.ValueAttribute;
        }
    }
}