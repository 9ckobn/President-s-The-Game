using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "RandomGetDefendEffect", menuName = "Effects/RandomGetDefendEffect")]
    public class SCRO_RandomGetDefendEffect : SCRO_Effect
    {
        [BoxGroup("Target")]
        [Label("Кто выбирает цель эффекта")]
        public TypeSelectTarget TypeSelectTarget;

        [BoxGroup("Target")]
        [ShowIf("TypeSelectTarget", TypeSelectTarget.Game)]
        [Label("Какие объекты под защитой")]
        public TypeAttribute[] ProtectAttributes;

        [BoxGroup("Random")]
        [Label("Какой атрибут использовать для рандома")]
        public TypeAttribute RandomAttribute;
    }
}