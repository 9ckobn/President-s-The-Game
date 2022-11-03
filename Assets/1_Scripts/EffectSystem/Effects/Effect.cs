using EffectSystem.SCRO;

namespace EffectSystem
{
    public class Effect
    {
        public TypeTargetEffect TypeTarget { get; private set; } // ����� �� �������� ��������� ������
        public TypeSelectTarget TypeSelectTarget { get; private set; } // ��� �������� ���� �������

        public TypeTimeApply TimeApply { get; private set; } // ����� ����������� ������?
        public int TimeStart { get; private set; }
        public TypeTimeApply TimeCancel { get; private set; } // ����� ���������� ������?
        public int TimeDurationEffect { get; private set; } // -1: ����������, 0: ���� �����, 2: ���� � ��������� �����

        public TypeCondition ApplyCondition { get; private set; }
        public TypeAttribute[] TypeAttributeApplyCondition { get; private set; }
        public TypeCondition CancelCondition { get; private set; }
        public TypeAttribute[] TypeAttributeCondition { get; private set; }
        public int CountTimes { get; private set; }

        public Effect(SCRO_Effect data)
        {
            TypeTarget = data.TypeTarget;
            TypeSelectTarget = data.TypeSelectTarget;

            TimeApply = data.TimeApply;
            TimeStart = data.TimeStart;
            TimeCancel = data.TimeCancel;
            TimeDurationEffect = data.TimeDurationEffect;

            ApplyCondition = data.ApplyCondition;
            TypeAttributeApplyCondition = data.TypeAttributeCancelCondition;
            CancelCondition = data.ApplyCondition;
            TypeAttributeCondition = data.TypeAttributeCancelCondition;
            CountTimes = data.CountTimes;
        }
    }
}