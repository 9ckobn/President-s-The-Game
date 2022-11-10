using Data;

namespace EffectSystem
{
    public class CancelEffect
    {
        public delegate void AfterCancel();
        public event AfterCancel EndCancelEvent;

        private CharacterData characterData;
        private Effect effect;

        public void Cancel(CharacterData characterData, Effect effect)
        {
            this.characterData = characterData;
            this.effect = effect;

            if(effect is DefendEffect)
            {
                DefendEffect defendEffect = effect as DefendEffect;

                foreach (var type in defendEffect.TypeDefends)
                {
                    characterData.DecreaseDefend(type,
                        (int)(characterData.GetValueAttribute(defendEffect.TypeNeedAttribute) / 100f * defendEffect.ValueAttribute));
                }
            }
        }
    }
}