using Core;
using Data;
using Gameplay;
using System.Collections.Generic;

namespace EffectSystem
{
    public class ApplyAttackEffect : ApplyEffect
    {
        private AttackEffect effect;
        private CharacterData attackData, defendData;
        private List<TypeAttribute> selectedAttributes;

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as AttackEffect;
            targetAttributes = new List<TypeAttribute>();
            selectedAttributes = new List<TypeAttribute>();

            SetAttackdefendData();

            if (effect.TypeSelectTarget == TypeSelectTarget.Game)
            {
                foreach (var type in effect.TypeTargetObject)
                {
                    selectedAttributes.Add(type);
                }

                ApplyDamage();
            }
            else if (effect.TypeSelectTarget == TypeSelectTarget.Player)
            {
                targetAttributes.Add(TypeAttribute.Economic);
                targetAttributes.Add(TypeAttribute.Food);
                targetAttributes.Add(TypeAttribute.Medicine);
                targetAttributes.Add(TypeAttribute.RawMaterials);

                foreach (var target in targetAttributes)
                {
                    defendData.ShowTargetAttribute(target);
                }
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
            DisableStateTarget();

            // Building have random defend. Count random

            if (defendData.AttributeHaveDefend(targetAttribute))
            {
                // TODO: check god defend

                defendData.LoseDefend(targetAttribute);

                EndApply();

            }
            else
            {
                selectedAttributes.Add(targetAttribute);

                ApplyDamage();
            }
        }

        private void ApplyDamage()
        {
            int damage = effect.BaseValue;

            if (effect.IsNeedAttribute)
            {
                damage += (int)(attackData.GetValueAttribute(effect.TypeAttribute) / 100f * effect.ValueAttribute);
            }

            foreach (var targetAttribute in selectedAttributes)
            {
                defendData.DownAttribute(targetAttribute, damage, true);
            }

            EndApply();
        }

        private void LoseAttack()
        {
            DisableStateTarget();

            EndApply();
        }

        private void DisableStateTarget()
        {
            foreach (var target in targetAttributes)
            {
                defendData.HideTargetAttribute(target);
            }
        }

        public override void StopApplyEffect()
        {
            DisableStateTarget();
        }
    }
}