using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "OtherEffect", menuName = "Effects/OtherEffect")]
    public class SCRO_OtherEffect : SCRO_Effect
    {
        [BoxGroup("Type effect")]
        [Label("��� �������� ���� �������")]
        public TypeOtherEffect TypeOtherEffect;

        [BoxGroup("UpAttributeAfterAttack")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.UpAttributeAfterAttack)]
        [Label("����� % �� ����� ����������")]
        public int ProcentAttack;

        [BoxGroup("UpAttributeAfterAttack")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.UpAttributeAfterAttack)]
        [Label("����� ������� �����������")]
        public TypeAttribute UpAttribute;
    }
}