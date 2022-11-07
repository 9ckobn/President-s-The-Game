using Core;
using Gameplay;

namespace EffectSystem
{
    public class ApplyRandomDefendEffect : ApplyRandomEffect
    {
        private RandomDefendEffect effect;

        private bool lucky;
        public bool GetLucky { get => lucky; }

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as RandomDefendEffect;
            characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;

            foreach (var typeDefend in effect.TypeDefends)
            {
                int procentRandom = characterData.GetValueAttribute(effect.RandomAttribute);

                characterData.AddDefend(typeDefend, true, effect.ValueDefend, procentRandom);
            }

            EndApply();
        }

        public override void SelectTargetBuilding(TypeAttribute targetAttribute)
        {
            throw new System.NotImplementedException();
        }

        public override void StopApplyEffect()
        {
            throw new System.NotImplementedException();
        }
    }
}