using EffectSystem.SCRO;

namespace EffectSystem
{
    public class Effect
    {
        public TypeTargetEffect TypeTarget { get; private set; } // ����� �� �������� ��������� ������
        public TypeSelectTarget TypeSelectTarget { get; private set; } // ��� �������� ���� �������
        public TypeTimeDuration TypeTimeDuration { get; private set; } // �� ������ ������� ����� ������������
        public TypeTimeApply TimeApply { get; private set; } // ����� ����������� ������?

        public int TimeStart { get; private set; }
        public int TimeDuration { get; private set; } // -1: ����������, 0: ���� �����, 2: ���� � ��������� �����

        public TypeCondition Condition { get; private set; }
        public TypeAttribute TypeAttributeAttack { get; private set; }

        public Effect(SCRO_Effect data)
        {
            TypeTarget = data.TypeTarget;
            TypeSelectTarget = data.TypeSelectTarget;
            TypeTimeDuration = data.TypeTimeDuration;
            TimeApply = data.TimeApply;

            TimeStart = data.TimeStart;
            TimeDuration = data.TimeDuration;

            Condition = data.Condition;
            TypeAttributeAttack = data.TypeAttributeAttack;
        }
    }
}