using EffectSystem.SCRO;

namespace EffectSystem
{
    public class DefendEffect : Effect
    {
        public TypeAttribute TypeDefend { get; private set; } // ����� ������ �������
        public bool Immortal { get; private set; } // ������ �� �������� �����?
        public int ValueDefend { get; private set; } // ������� % ������ ��������
        public int DurationProtect { get; private set; } // ������� ���� ������ ������

        public DefendEffect(SCRO_DefendEffect data) : base(data)
        {
            TypeDefend = data.TypeProtect;
            Immortal = data.Immortal;
            ValueDefend = data.ValueProtect;
            DurationProtect = data.DurationProtect;
        }
    }
}