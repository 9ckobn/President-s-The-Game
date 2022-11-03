using NaughtyAttributes;
using UnityEngine;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "BuffEffect", menuName = "Effects/BuffEffect")]
    public class SCRO_BuffEffect : SCRO_Effect
    {
        [BoxGroup("Buff")]
        [Label("����� buff")]
        public TypeBuff TypeBuff;

        [BoxGroup("Buff")]
        [HideIf("TypeBuff", TypeBuff.UpAttack)]
        [Label("������ �� ������� ��������� ����")]
        public TypeAttribute[] TypeTargetObject;

        [BoxGroup("Buff")]
        [Label("������� �������� ����")]
        public int BaseValue;

        [BoxGroup("Buff")]
        [Label("��������� �������� ��������?")]
        public TypeAttribute TypeAttribute;

        [BoxGroup("Buff")]
        [Label("�������� � % �� ��������")]
        public int ValueAttribute;
    }
}