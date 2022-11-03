using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "RandomGetDefendEffect", menuName = "Effects/RandomGetDefendEffect")]
    public class SCRO_RandomDefendEffect : SCRO_DefendEffect
    {
        [BoxGroup("Random")]
        [Label("����� ������� ������������ ��� �������")]
        public TypeAttribute RandomAttribute;
    }
}