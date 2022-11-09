using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "DefendEffect", menuName = "Effects/DefendEffect")]
    public class SCRO_DefendEffect : SCRO_Effect
    {
        [BoxGroup("Protect")]
        [ShowIf("TypeSelectTarget", TypeSelectTarget.Game)]
        [Label("����� ������� ��������")]
        public TypeAttribute[] TypeDefends;

        [BoxGroup("Value")]
        [Label("������������ ������?")]
        public bool IsGodDefend;

        [BoxGroup("Value")]
        [Label("������� �������� �������")]
        [HideIf("IsGodDefend")]
        public int BaseValue;

        [BoxGroup("Value")]
        [HideIf("IsGodDefend")]
        [Label("��������� �������� ��������")]
        public TypeAttribute TypeNeedAttribute;

        [BoxGroup("Value")]
        [HideIf("IsGodDefend")]
        [Label("�������� � % �� ��������")]
        public int ValueAttribute;
    }
}