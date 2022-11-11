using Data;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
    public class CheckEffectAfterEvent
    {
        private CancelEffect cancelEffect;

        public CheckEffectAfterEvent()
        {
            cancelEffect = new CancelEffect();
        }

        public void CheckEndRound(CharacterData characterData)
        {
            List<Effect> effects = characterData.TemporaryEffects;
            List<Effect> activeEffects = new List<Effect>();

            foreach (var effect in effects)
            {
                activeEffects.Add(effect);
            }

            foreach (var effect in effects)
            {
                if (effect.TimeCancel == TypeTimeApply.AfterTime)
                {
                    effect.DecreaseCurrentTimeDuration();

                    if (effect.CurrentTimeDuration <= 0)
                    {
                        activeEffects.Remove(effect);
                        cancelEffect.Cancel(characterData, effect);
                    }
                }
            }

            characterData.TemporaryEffects = activeEffects;
        }

        public void CheckEvent(CharacterData characterData, TypeCondition typeCondition)
        {
            List <Effect> effects = characterData.TemporaryEffects;

            if (effects.Count > 0)
            {
                foreach (var effect in effects)
                {
                    if (effect.TimeApply == TypeTimeApply.Condition)
                    {
                        foreach (var type in effect.TypesApplyCondition)
                        {

                        }
                    }
                    else if (effect.TimeCancel == TypeTimeApply.Condition)
                    {

                    }
                }
            }
        }
    }
}