using Core;
using EffectSystem;
using EffectSystem.SCRO;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.Data
{
    public class CardFightData : CardDataBase
    {
        public string Description { get; private set; }
        public TypeAttribute[] TypeCost { get; private set; }
        public int Cost { get; private set; }

        private int reloading;
        private List<Effect> effects = new List<Effect>();

        public int CurrentReloading { get; private set; }
        public List<Effect> GetEffects { get => effects; }

        public CardFightData(SCRO_CardFight data, Sprite sprite, SCRO_Effect[] effects) : base(data.Id.ToString(), sprite, data.name)
        {
            Description = data.Description;
            TypeCost = data.TypeCost;
            Cost = data.Cost;
            reloading = data.Reloading;
            CurrentReloading = 0;

            if(effects.Length == 0)
            {
                BoxController.GetController<LogController>().LogError($"Not have effects in {data.Name}");
            }

            foreach (var dataEffect in effects)
            {
                Effect effect = null;

                if(dataEffect is SCRO_AttackEffect)
                {
                    effect = new AttackEffect(dataEffect as SCRO_AttackEffect);
                }
                else if(dataEffect is SCRO_BuffEffect)
                {
                    effect = new BuffEffect(dataEffect as SCRO_BuffEffect);
                }
                else if (dataEffect is SCRO_OtherEffect)
                {
                    effect = new OtherEffect(dataEffect as SCRO_OtherEffect);
                }
                else if (dataEffect is SCRO_RandomDefendEffect)
                {
                    effect = new RandomDefendEffect(dataEffect as SCRO_RandomDefendEffect);
                }
                else if (dataEffect is SCRO_DefendEffect)
                {
                    effect = new DefendEffect(dataEffect as SCRO_DefendEffect);
                }
                else if (dataEffect is SCRO_RandomUpAttributeEffect)
                {
                    effect = new RandomUpAttributeEffect(dataEffect as SCRO_RandomUpAttributeEffect);
                }

                if(effect == null)
                {
                    BoxController.GetController<LogController>().LogError($"Effect not create! Effect name - {dataEffect.name}");
                }
                else
                {
                    this.effects.Add(effect);
                }
            }
        }

        public void UpdateReloading()
        {
            CurrentReloading = reloading;
        }

        public void DecreaseReloading()
        {
            if (CurrentReloading > 0)
            {
                CurrentReloading--;
            }
        }
    }
}