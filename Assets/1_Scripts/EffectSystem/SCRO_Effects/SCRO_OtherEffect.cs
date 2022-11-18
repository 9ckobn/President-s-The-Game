using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "OtherEffect", menuName = "Effects/OtherEffect")]
    public class SCRO_OtherEffect : SCRO_Effect
    {
        [BoxGroup("Type effect")]
        [Label("Тип эффекта")]
        public TypeOtherEffect TypeOtherEffect;

        [BoxGroup("UpAttributeAfterAttack")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.VampirismAfterAttack)]
        [Label("Какой % от атаки используем")]
        public int ProcentAttack;

        [BoxGroup("UpAttributeAfterAttack")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.VampirismAfterAttack)]
        [Label("Какой атрибут увеличиваем")]
        public TypeAttribute UpAttribute;
        
        [BoxGroup("Loan")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.Loan)]
        [Label("Какой атрибут используем в качестве займа")]
        public TypeAttribute TypeAttributeLoan;

        [BoxGroup("Loan")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.Loan)]
        [Label("Какой % атрибуты займа")]
        public int ProcentLoan;

        [BoxGroup("Loan")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.Loan)]
        [Label("Какие атрибуты получат урон после займа")]
        public TypeAttribute[] TypeAttributesAfterLoan;

        [BoxGroup("Loan")]
        [ShowIf("TypeOtherEffect", TypeOtherEffect.Loan)]
        [Label("Какой % урона атрибуты получат")]
        public int ValueProcentDamageLoan;
    }
}