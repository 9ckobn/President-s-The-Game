using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "AttackEffect", menuName = "Effects/AttackEffect")]
    public class SCRO_AttackEffect : SCRO_Effect
    {
        [BoxGroup("Target")]
        [Label("��� �������� ���� �������")]
        public TypeSelectTarget TypeSelectTarget;
        [BoxGroup("Target")]
        [Label("������ �� ������� ��������� ������")]
        public TypeFactor[] TypeTargetObject;

        #region VALUE

        [BoxGroup("Value")]
        [Label("������� �������� �������")]
        public int BaseValue;
        [BoxGroup("Value")]
        [Label("���� �������� �� ��������?")]
        public bool IsNeedAttribute;
        [BoxGroup("Value")]
        [ShowIf("IsNeedAttribute")]
        [Label("��������� �������� ��������")]
        public TypeAttribute TypeAttribute;
        [BoxGroup("Value")]
        [ShowIf("IsNeedAttribute")]
        [Label("�������� � % �� ���������")]
        public int ValueAttribute;

        #endregion
    }
}