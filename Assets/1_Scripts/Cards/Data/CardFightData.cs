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
        public int Cost { get; private set; }

        private int reloading;
        private List<Effect> effects = new List<Effect>();

        public int CurrentReloading { get; private set; }
        public List<Effect> GetEffects { get => effects; }

        public CardFightData(SCRO_CardFight data, Sprite sprite, SCRO_Effect[] effects) : base(data.Id.ToString(), sprite, data.name)
        {
            Description = data.Description;
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
                else if (dataEffect is SCRO_ProtectEffect)
                {
                    effect = new ProtectEffect(dataEffect as SCRO_ProtectEffect);
                }
                else if (dataEffect is SCRO_RandomGetProtectEffect)
                {
                    effect = new RandomGetProtectEffect(dataEffect as SCRO_RandomGetProtectEffect);
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