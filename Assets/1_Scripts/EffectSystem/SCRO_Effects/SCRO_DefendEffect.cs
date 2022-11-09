using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "DefendEffect", menuName = "Effects/DefendEffect")]
    public class SCRO_DefendEffect : SCRO_Effect
    {
        [BoxGroup("Protect")]
        [ShowIf("TypeSelectTarget", TypeSelectTarget.Game)]
        [Label("Какие объекты защищёны")]
        public TypeAttribute[] TypeDefends;

        [BoxGroup("Value")]
        [Label("Божественная защита?")]
        public bool IsGodDefend;

        [BoxGroup("Value")]
        [Label("Базовое значение эффекта")]
        [HideIf("IsGodDefend")]
        public int BaseValue;

        [BoxGroup("Value")]
        [HideIf("IsGodDefend")]
        [Label("Добавляем значение атрибута")]
        public TypeAttribute TypeNeedAttribute;

        [BoxGroup("Value")]
        [HideIf("IsGodDefend")]
        [Label("Значение в % от атрибута")]
        public int ValueAttribute;
    }
}