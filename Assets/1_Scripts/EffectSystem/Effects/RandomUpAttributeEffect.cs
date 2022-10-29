using EffectSystem.SCRO;

namespace EffectSystem
{
    public class RandomUpAttributeEffect : Effect
    {
        public int Value { get; private set; }
        public bool IsNeedAttribute { get; private set; }
        public TypeAttribute TypeAttribute { get; private set; }
        public int ValueAttribute { get; private set; }
        public TypeAttribute TypeWinAttribute { get; private set; } // ����� ���������� ������������� ��� ������
        public int WinProcent { get; private set; } // �� ������� % �����������
        public TypeAttribute TypeLoseAttribute { get; private set; } // ����� ���������� ����������� ��� ���������
        public int LoseProcent { get; private set; } // �� ������� % �����������

        public RandomUpAttributeEffect(SCRO_RandomUpAttributeEffect data) : base(data)
        {
            Value = data.Value;
            IsNeedAttribute = data.IsNeedAttribute;
            TypeAttribute = data.TypeAttribute;
            ValueAttribute = data.ValueAttribute;
            WinProcent = data.WinProcent;
            TypeLoseAttribute = data.TypeLoseAttribute;
            LoseProcent = data.LoseProcent;
        }
    }
}