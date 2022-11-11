using Data;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
    public class CheckEffectAfterEvent
    {
        private CancelEffect cancelEffect;

        private List<Effect> currentEffects;
        private List<Effect> activeEffects;
        private CharacterData currentCharacterData;

        public CheckEffectAfterEvent()
        {
            cancelEffect = new CancelEffect();
        }

        public void CheckEndRound(CharacterData characterData)
        {
            CreateCurrentEffects(characterData);

            foreach (var effect in currentEffects)
            {
                if (effect.TimeCancel == TypeTimeApply.AfterTime)
                {
                    effect.DecreaseCurrentTimeDuration();

                    if (effect.CurrentTimeDuration <= 0)
                    {
                        RemoveEffect(effect);
                    }
                }
            }

            characterData.TemporaryEffects = activeEffects;
        }

        public void CheckEvent(CharacterData characterData, TypeCondition typeEvent)
        {
            CreateCurrentEffects(characterData);

            foreach (var effect in currentEffects)
            {
                if (effect.TimeApply == TypeTimeApply.Condition)
                {

                }

                if (effect.TimeCancel == TypeTimeApply.Condition)
                {
                    if (effect.CancelCondition == typeEvent)
                    {
                        RemoveEffect(effect);
                    }
                }
            }
            
            characterData.TemporaryEffects = activeEffects;
        }

        private void CreateCurrentEffects(CharacterData characterData)
        {
            currentCharacterData = characterData;
            currentEffects = characterData.TemporaryEffects;
            activeEffects = new List<Effect>();

            foreach (var effect in currentEffects)
            {
                activeEffects.Add(effect);
            }
        }

        private void RemoveEffect(Effect effect)
        {
            Debug.Log($"remove effect {effect.Id}");
            activeEffects.Remove(effect);
            cancelEffect.Cancel(currentCharacterData, effect);
        }
    }
}