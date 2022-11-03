using Buildings;
using Core;
using Data;
using Gameplay;

namespace EffectSystem
{
    public class ApplyDefendEffect : ApplyEffect
    {
        private DefendEffect effect;

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as DefendEffect;

            AddEffect();
        }

        public override void SelectTargetBuilding(Building building)
        {
        }

        private void AddEffect()
        {
            CharacterData characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;
            characterData.AddTemporaryEffect(effect);

            foreach (var typeDefend in effect.TypeDefends)
            {
                characterData.ShowDefend(typeDefend);
            }

            EndApply();
        }
    }
}