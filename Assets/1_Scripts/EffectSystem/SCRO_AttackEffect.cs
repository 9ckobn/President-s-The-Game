using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "AttackEffect", menuName = "Effects/AttackEffect")]
    public class SCRO_AttackEffect : SCRO_Effect
    {
        [BoxGroup("Target")]
        [Label("Кто выбирает цель эффекта")]
        public TypeSelectTarget TypeSelectTarget;
        [BoxGroup("Target")]
        [Label("Объект на который действует эффект")]
        public TypeFactor[] TypeTargetObject;

        #region VALUE

        [BoxGroup("Value")]
        [Label("Базовое значение эффекта")]
        public int BaseValue;
        [BoxGroup("Value")]
        [Label("Есть усиление от атрибута?")]
        public bool IsNeedAttribute;
        [BoxGroup("Value")]
        [ShowIf("IsNeedAttribute")]
        [Label("Добавляем значение атрибута")]
        public TypeAttribute TypeAttribute;
        [BoxGroup("Value")]
        [ShowIf("IsNeedAttribute")]
        [Label("Значение в % от аттрибута")]
        public int ValueAttribute;

        #endregion
    }
}