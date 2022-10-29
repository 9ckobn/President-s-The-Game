using EffectSystem.SCRO;

namespace EffectSystem
{
    public class Effect
    {
        public TypeTargetEffect TypeTarget { get; private set; } // Игрок на которого действует эффект
        public TypeTimeApply TimeApply { get; private set; } // Когда применяется эффект?

        public int TimeStart { get; private set; }
        public int TimeDuration { get; private set; } // -1: бесконечно, 0: этот раунд, 2: этот и следующий раунд

        public TypeCondition Condition { get; private set; }
        public TypeAttribute TypeAttributeAttack { get; private set; }

        public Effect(SCRO_Effect data)
        {
            TypeTarget = data.TypeTarget;
            TimeApply = data.TimeApply;

            TimeStart = data.TimeStart;
            TimeDuration = data.TimeDuration;

            Condition = data.Condition;
            TypeAttributeAttack = data.TypeAttributeAttack;
        }
    }
}