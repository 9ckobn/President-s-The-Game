using EffectSystem.SCRO;

namespace EffectSystem
{
    public class ProtectEffect : Effect
    {
        public TypeAttribute TypeProtect { get; private set; } // ����� ������ �������
        public bool Immortal { get; private set; } // ������ �� �������� �����?
        public int ValueProtect { get; private set; } // ������� % ������ ��������
        public int DurationProtect { get; private set; } // ������� ���� ������ ������

        public ProtectEffect(SCRO_ProtectEffect data) : base(data)
        {
            TypeProtect = data.TypeProtect;
            Immortal = data.Immortal;
            ValueProtect = data.ValueProtect;
            DurationProtect = data.DurationProtect;
        }
    }
}
