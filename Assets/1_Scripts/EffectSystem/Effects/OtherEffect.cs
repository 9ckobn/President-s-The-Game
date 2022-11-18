using EffectSystem.SCRO;

namespace EffectSystem
{
    public class OtherEffect : Effect
    {
        public TypeOtherEffect TypeOtherEffect { get; private set; }
        public int ProcentAttack { get; private set; } // ����� % �� ����� ����������
        public TypeAttribute UpAttribute { get; private set; } // ����� ������� �����������
        public TypeAttribute TypeAttributeLoan { get; private set; } // ����� ������� ���������� � �������� �����
        public int ProcentLoan { get; private set; } // ����� % �������� �����
        public TypeAttribute[] TypeAttributesAfterLoan { get; private set; } // ����� �������� ������� ���� ����� �����
        public int ValueProcentDamageLoan { get; private set; } // ����� % ����� �������� �������
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