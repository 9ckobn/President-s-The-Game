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
        public TypeAttribute[] TypesTargetObject;

        [BoxGroup("Buff")]
        [HideIf("TypeBuff", TypeBuff.Discount)]
        [Label("������� �������� ����")]
        public int BaseValue;

        [BoxGroup("Buff")]
        [HideIf("TypeBuff", TypeBuff.Discount)]
        [Label("��������� �������� ��������?")]
        public TypeAttribute TypeAttribute;

        [BoxGroup("Buff")]
        [HideIf("TypeBuff", TypeBuff.Discount)]
        [Label("�������� � % �� ��������")]
        public int ValueAttribute;
    }
}