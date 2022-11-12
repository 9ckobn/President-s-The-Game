using Core;
using Data;
using Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
    public class ApplyAttackEffect : ApplyEffect
    {
        private AttackEffect effect;
        private CharacterData attackData, defendData;

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as AttackEffect;
            targetAttributes = new List<TypeAttribute>();

            SetAttackdefendData();

            if (effect.TypeSelectTarget == TypeSelectTarget.Game)
            {
                foreach (var type in effect.TypeTargetObject)
                {
                    if (defendData.AttributeHaveGodDefend(type))
                    {
                        defendData.LoseGodDefend(type);
                    }
                    else
                    {
                        ApplyDamage(type);
                    }
                }

                EndApply();
            }
            else if (effect.TypeSelectTarget == TypeSelectTarget.Player)
            {
                ShowTargetBuildings(defendData);
            }
        }

        private void SetAttackdefendData()
        {
            if (isPlayer && effect.TypeTarget == TypeTargetEffect.Enemy)
            {
                defendData = BoxController.GetController<CharactersDataController>().GetEnemyData;
            }
            else if (isPlayer && effect.TypeTarget == TypeTargetEffect.Himself)
            {
                defendData = BoxController.GetController<CharactersDataController>().GetPlayerData;
            }
            else if (!isPlayer && effect.TypeTarget == TypeTargetEffect.Enemy)
            {
                defendData = BoxController.GetController<CharactersDataController>().GetPlayerData;
            }
            else if (!isPlayer && effect.TypeTarget == TypeTargetEffect.Himself)
            {
                defendData = BoxController.GetController<CharactersDataController>().GetEnemyData;
            }
            else
            {
                BoxController.GetController<LogController>().LogError($"Not have logic apply effec!");
            }

            if (isPlayer)
            {
                attackData = BoxController.GetController<CharactersDataController>().GetPlayerData;
            }
            else
            {
                attackData = BoxController.GetController<CharactersDataController>().GetEnemyData;
            }
        }

        public override void SelectTargetBuilding(TypeAttribute targetAttribute)
        {
            HideTargetBuildings(defendData);

            if (defendData.AttributeHaveGodDefend(targetAttribute))
            {
                defendData.LoseGodDefend(targetAttribute);
            }
            else
            {
                ApplyDamage(targetAttribute);
            }

            EndApply();
        }

        private void ApplyDamage(TypeAttribute targetAttribute)
        {
            int damage = effect.BaseValue;
            int defend = defendData.GetValueDefend(targetAttribute);

            if (effect.IsNeedAttribute)
            {
                damage += (int)(attackData.GetValueAttribute(effect.TypeAttribute) / 100f * effect.ValueAttribute);
            }

            damage += CountBuffAttack();

            if (defend > 0)
            {
                defendData.DecreaseDefend(targetAttribute, damage);
            }

            if (damage > defend)
            {
                int damageAttribute = damage - defend;
                defendData.DownAttribute(targetAttribute, damageAttribute, true);
            }

            AdditionalDamage(damage);
            CountVimpirismEffects(damage);
        }

        public override void StopApplyEffect()
        {
            HideTargetBuildings(defendData);
        }

        private int CountBuffAttack()
        {
            int buff = 0;

            foreach (var effect in attackData.GetBuffEffects())
            {
                if (effect.TypeBuff == TypeBuff.UpAttack)
                {
                    buff = effect.BaseValue;
                    buff += (int)(attackData.GetValueAttribute(effect.TypeAttribute) / 100f * effect.ValueAttribute);
                }
            }

            return buff;
        }

        private void AdditionalDamage(int damage)
        {
            int additionalDamage = 0;

            foreach (var effect in attackData.GetBuffEffects())
            {
                additionalDamage = 0;

                if (effect.TypeBuff == TypeBuff.AdditionalDamage)
                {
                    additionalDamage = effect.BaseValue;
                    additionalDamage += (int)(attackData.GetValueAttribute(effect.TypeAttribute) / 100f * effect.ValueAttribute);
                }

                if (additionalDamage > 0)
                {
                    foreach (var typeTarget in effect.TypesTargetObject)
                    {
                        defendData.DownAttribute(typeTarget, additionalDamage);
                    }
                }
            }
        }

        private void CountVimpirismEffects(int damage)
        {
            foreach (var effect in attackData.GetOtherEffects())
            {
                int value = (int)(damage / 100f * effect.ProcentAttack);
                attackData.UpAttribute(effect.UpAttribute, value);
            }
        }
    }
}