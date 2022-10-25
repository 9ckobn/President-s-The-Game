using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "RandomGetProtectEffect", menuName = "Effects/RandomGetProtectEffect")]
    public class SCRO_RandomGetProtectEffect : SCRO_Effect
    {
        [BoxGroup("Random")]
        [Label(" акой атрибут использовать дл€ рандома")]
        public TypeAttribute RandomAttribute;
    }
}