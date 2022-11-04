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

        [BoxGroup("Protect")]
        [Label("Сколько % защиты получает")]
        public int ValueProtect = 100; // 100 - immortal
    }
}