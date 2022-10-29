using EffectSystem.SCRO;

namespace EffectSystem
{
    public class Effect
    {
        public TypeTargetEffect typeTarget { get; private set; } // Игрок на которого действует эффект
        public TypeTimeApply timeApply { get; private set; } // Когда применяется эффект?

        public int timeStart { get; private set; }
        public int timeDuration { get; private set; } // -1: бесконечно, 0: этот раунд, 2: этот и следующий раунд

        public TypeCondition condition { get; private set; }
        public TypeAttribute typeAttributeAttack { get; private set; }

        public Effect(SCRO_Effect data)
        {
            typeTarget = data.TypeTarget;
            timeApply = data.TimeApply;

            timeStart = data.TimeStart;
            timeDuration = data.TimeDuration;

            condition = data.Condition;
            typeAttributeAttack = data.TypeAttributeAttack;
        }
    }
}