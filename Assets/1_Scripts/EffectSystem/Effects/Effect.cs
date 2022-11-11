using EffectSystem.SCRO;

namespace EffectSystem
{
    public class Effect
    {
        public int Id { get; private set; }
        public TypeTargetEffect TypeTarget { get; private set; } // ����� �� �������� ��������� ������
        public TypeSelectTarget TypeSelectTarget { get; private set; } // ��� �������� ���� �������

        public TypeTimeApply TimeApply { get; private set; } // ����� ����������� ������?
        public int TimeStart { get; private set; }
        public TypeTimeApply TimeCancel { get; private set; } // ����� ���������� ������?
        private int timeDurationEffect; // -1: ����������, 0: ���� �����, 2: ���� � ��������� �����
        public int CurrentTimeDuration { get; private set; }

        public TypeCondition ApplyCondition { get; private set; }
        public TypeCondition CancelCondition { get; private set; }
        public int CountTimes { get; private set; }

        public void DecreaseCurrentTimeDuration()
        {
            CurrentTimeDuration--;
        }

        public Effect(SCRO_Effect data)
        {
            Id = data.Id;
            TypeTarget = data.TypeTarget;
            TypeSelectTarget = data.TypeSelectTarget;

            TimeApply = data.TimeApply;
            TimeStart = data.TimeStart;
            TimeCancel = data.TimeCancel;
            timeDurationEffect = data.TimeDurationEffect;
            CurrentTimeDuration = timeDurationEffect;

            ApplyCondition = data.ApplyCondition;
            CancelCondition = data.CancelCondition;
            CountTimes = data.CountTimes;
        }
    }
}