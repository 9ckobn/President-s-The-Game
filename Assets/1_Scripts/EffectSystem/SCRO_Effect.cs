using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem
{
    public class SCRO_Effect : ScriptableObject
    {
        [BoxGroup("Target")]
        [Label("Игрок на которого действует эффект")]
        public TypeTargetEffect TypeTarget;

        #region TIME

        [BoxGroup("Time")]
        [Label("Сработает в этот раунд")]
        public bool RightNow = true;
        [BoxGroup("Time")]
        [HideIf("RightNow")]
        [Label("Время старта эффекта")]
        public int TimeStart;
        [BoxGroup("Time")]
        [HideIf("RightNow")]
        [Label("Время продолжительности эффекта")]
        public int TimeDuration;

        #endregion
    }
}