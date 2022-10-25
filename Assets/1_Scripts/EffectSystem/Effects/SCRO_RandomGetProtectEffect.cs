using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "RandomGetProtectEffect", menuName = "Effects/RandomGetProtectEffect")]
    public class SCRO_RandomGetProtectEffect : SCRO_Effect
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