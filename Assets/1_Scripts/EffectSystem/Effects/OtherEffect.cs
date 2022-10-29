using EffectSystem.SCRO;

namespace EffectSystem
{
    public class OtherEffect : Effect
    {
        public TypeOtherEffect TypeOtherEffect { get; private set; }
        public int ProcentAttack { get; private set; } // ����� % �� ����� ����������
        public TypeAttribute UpAttribute { get; private set; } // ����� ������� �����������

        public OtherEffect(SCRO_OtherEffect data) : base(data)
        {
            TypeOtherEffect = data.TypeOtherEffect;
            ProcentAttack = data.ProcentAttack;
            UpAttribute = data.UpAttribute;
        }
    }
}