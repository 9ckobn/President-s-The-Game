using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem.SCRO
{
    public class SCRO_Effect : ScriptableObject
    {
        [BoxGroup("Id")]
        public int Id;

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
        [Label("Время отмены эфекта")]
        public TypeTimeApply TimeCancel;

        [BoxGroup("Time")]
        [ShowIf("TimeCancel", TypeTimeApply.AfterTime)]
        [Label("Активен в течении n раундов")]
        public int TimeDurationEffect;

        #endregion

        #region CONDITION

        [BoxGroup("Condition")]
        [ShowIf("TimeApply", TypeTimeApply.Condition)]
        [Label("Тип условия применения")]
        public TypeCondition ApplyCondition = TypeCondition.None;

        //[BoxGroup("Condition")]
        //[ShowIf("TimeApply", TypeTimeApply.Condition)]
        //[Label("На какие объекты действует применение эфекта")]
        //public TypeAttribute[] TypeAttributeApplyCondition;

        [BoxGroup("Condition")]
        [ShowIf("TimeCancel", TypeTimeApply.Condition)]
        [Label("Тип условия отмены")]
        public TypeCondition CancelCondition = TypeCondition.None;

        [BoxGroup("Condition")]
        [ShowIf("TimeCancel", TypeTimeApply.Condition)]
        [Label("На какие объекты действует отмена эфекта")]
        public TypeAttribute[] TypeAttributeCancelCondition;

        [BoxGroup("Condition")]
        [ShowIf("TimeCancel", TypeTimeApply.Condition)]
        [Label("Сколько раз должно примениться")]
        public int CountTimes = 1;

        #endregion
    }
}