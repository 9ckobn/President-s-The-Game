using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "DefendEffect", menuName = "Effects/DefendEffect")]
    public class SCRO_DefendEffect : SCRO_Effect
    {
        [BoxGroup("Protect")]
        [Label("Какой объект защищён")]
        public TypeAttribute TypeProtect;

        [BoxGroup("Protect")]
        [Label("Совсем не получает урона?")]
        public bool Immortal = true;

        [BoxGroup("Protect")]
        [HideIf("Immortal")]
        [Label("Сколько % защиты получает")]
        public int ValueProtect;

        [BoxGroup("Protect")]
        [Label("Сколько атак длится защита")]
        public int DurationProtect;
    }
}