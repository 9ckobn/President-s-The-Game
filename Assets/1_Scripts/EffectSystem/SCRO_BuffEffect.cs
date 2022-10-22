using NaughtyAttributes;
using UnityEngine;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "BuffEffect", menuName = "Effects/BuffEffect")]
    public class SCRO_BuffEffect : SCRO_Effect
    {
        [BoxGroup("Buff")]
        [Label("����� buff")]
        public TypeBuff TypeBuff;
        [BoxGroup("Buff")]
        [ShowIf("TypeBuff", TypeBuff.AdditionalDamage)]
        [Label("������ �� ������� ��������� ����")]
        public TypeFactor[] TypeTargetObject;
        [BoxGroup("Buff")]
        [Label("������� �������� ����")]
        public int BaseValue;
        [BoxGroup("Buff")]
        [Label("��������� �������� ��������")]
        public TypeAttribute TypeAttribute;
        [BoxGroup("Buff")]
        [Label("�������� � % �� ���������")]
        public int ValueAttribute;
    }
}