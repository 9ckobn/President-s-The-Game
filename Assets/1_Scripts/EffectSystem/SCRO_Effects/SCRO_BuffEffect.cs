using NaughtyAttributes;
using UnityEngine;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "BuffEffect", menuName = "Effects/BuffEffect")]
    public class SCRO_BuffEffect : SCRO_Effect
    {
        [BoxGroup("Buff")]
        [Label("Какой buff")]
        public TypeBuff TypeBuff;

        [BoxGroup("Buff")]
        [HideIf("TypeBuff", TypeBuff.UpAttack)]
        [Label("Объект на который действует бафф")]
        public TypeAttribute[] TypesTargetObject;

        [BoxGroup("Buff")]
        [HideIf("TypeBuff", TypeBuff.Discount)]
        [Label("Базовое значение бафа")]
        public int BaseValue;

        [BoxGroup("Buff")]
        [HideIf("TypeBuff", TypeBuff.Discount)]
        [Label("Добавляем значение атрибута?")]
        public TypeAttribute TypeAttribute;

        [BoxGroup("Buff")]
        [HideIf("TypeBuff", TypeBuff.Discount)]
        [Label("Значение в % от атрибута")]
        public int ValueAttribute;
    }
}