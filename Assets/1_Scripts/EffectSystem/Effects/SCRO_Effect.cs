using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem
{
    public class SCRO_Effect : ScriptableObject
    {
        [BoxGroup("Target")]
        [Label("Игрок на которого действует эффект")]
        public TypeTargetEffect TypeTarget;

        [BoxGroup("Target")]
        [Label("Кто выбирает цель эффекта")]
        public TypeSelectTarget TypeSelectTarget;

        #region TIME

        [BoxGroup("Time")]
        [Label("Когда применяется эффект?")]
        public TypeTimeApply TimeApply;

        [BoxGroup("Time")]
        [ShowIf("TimeApply", TypeTimeApply.AfterTime)]
        [Label("Время старта эффекта")]
        public int TimeStart;

        [BoxGroup("Time")]
        [Label("Время продолжительности эффекта")]
        public int TimeDuration = 1; // -1: бесконечно, 0: этот раунд, 2: этот и следующий раунд

        #endregion

        #region CONDITION

        [BoxGroup("Condition")]
        [ShowIf("TimeApply", TypeTimeApply.Condition)]
        [Label("Тип условия применения")]
        public TypeCondition Condition = TypeCondition.None;

        [BoxGroup("Condition")]
        [ShowIf("Condition", TypeCondition.Attack)]
        [Label("Какой объект атакуется?")]
        public TypeAttribute TypeAttributeAttack;

        #endregion
    }
}