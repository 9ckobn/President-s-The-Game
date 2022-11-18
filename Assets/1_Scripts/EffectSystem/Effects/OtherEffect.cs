using EffectSystem.SCRO;

namespace EffectSystem
{
    public class OtherEffect : Effect
    {
        public TypeOtherEffect TypeOtherEffect { get; private set; }
        public int ProcentAttack { get; private set; } // Какой % от атаки используем
        public TypeAttribute UpAttribute { get; private set; } // Какой атрибут увеличиваем
        public TypeAttribute TypeAttributeLoan { get; private set; } // Какой атрибут используем в качестве займа
        public int ProcentLoan { get; private set; } // Какой % атрибуты займа
        public TypeAttribute[] TypeAttributesAfterLoan { get; private set; } // Какие атрибуты получат урон после займа
        public int ValueProcentDamageLoan { get; private set; } // Какой % урона атрибуты получат
        public int ValueDamageLoan { get; set; }

        public OtherEffect(SCRO_OtherEffect data) : base(data)
        {
            TypeOtherEffect = data.TypeOtherEffect;
            ProcentAttack = data.ProcentAttack;
            UpAttribute = data.UpAttribute;
            TypeAttributeLoan = data.TypeAttributeLoan;
            ProcentLoan = data.ProcentLoan;
            TypeAttributesAfterLoan = data.TypeAttributesAfterLoan;
            ValueProcentDamageLoan = data.ValueProcentDamageLoan;
        }
    }
}