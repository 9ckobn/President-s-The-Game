using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "OtherEffect", menuName = "Effects/OtherEffect")]
    public class SCRO_OtherEffect : SCRO_Effect
    {
        [BoxGroup("Type effect")]
        [Label("Кто выбирает цель эффекта")]
        public TypeOtherEffect TypeOtherEffect;

        [BoxGroup("UpAttributeAfterAttack")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.UpAttributeAfterAttack)]
        [Label("Какой % от атаки используем")]
        public int ProcentAttack;

        [BoxGroup("UpAttributeAfterAttack")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.UpAttributeAfterAttack)]
        [Label("Какой атрибут увеличиваем")]
        public TypeAttribute UpAttribute;
    }
}