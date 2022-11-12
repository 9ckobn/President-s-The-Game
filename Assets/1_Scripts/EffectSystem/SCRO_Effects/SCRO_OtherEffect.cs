using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "OtherEffect", menuName = "Effects/OtherEffect")]
    public class SCRO_OtherEffect : SCRO_Effect
    {
        [BoxGroup("Type effect")]
        [Label("��� �������")]
        public TypeOtherEffect TypeOtherEffect;

        [BoxGroup("UpAttributeAfterAttack")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.VampirismAfterAttack)]
        [Label("����� % �� ����� ����������")]
        public int ProcentAttack;

        [BoxGroup("UpAttributeAfterAttack")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.VampirismAfterAttack)]
        [Label("����� ������� �����������")]
        public TypeAttribute UpAttribute;
    }
}