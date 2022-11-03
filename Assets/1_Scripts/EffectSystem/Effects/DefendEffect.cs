using EffectSystem.SCRO;

namespace EffectSystem
{
    public class DefendEffect : Effect
    {
        public TypeAttribute[] TypeDefends { get; private set; } // ����� ������ �������
        public int ValueDefend { get; private set; } // ������� % ������ ��������

        public DefendEffect(SCRO_DefendEffect data) : base(data)
        {
            TypeDefends = data.TypeDefends;
            ValueDefend = data.ValueProtect;
        }
    }
}