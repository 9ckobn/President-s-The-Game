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
        public TypeAttribute[] TypeTargetObject;

        [BoxGroup("Buff")]
        [Label("Базовое значение бафа")]
        public int BaseValue;

        [BoxGroup("Buff")]
        [Label("Добавляем значение атрибута?")]
        public TypeAttribute TypeAttribute;

        [BoxGroup("Buff")]
        [Label("Значение в % от атрибута")]
        public int ValueAttribute;
    }
}