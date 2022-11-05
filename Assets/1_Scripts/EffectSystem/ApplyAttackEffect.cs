using Buildings;
using Core;
using Data;
using Gameplay;
using SceneObjects;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace EffectSystem
{
    public class ApplyAttackEffect : ApplyEffect
    {
        private AttackEffect effect;
        private CharacterData attackData, defendData;
        private List<Building> targetBuildings;

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as AttackEffect;
            targetAttributes.Clear();

            SetAttackdefendData();

            if (effect.TypeSelectTarget == TypeSelectTarget.Game)
            {
                GameSelectTarget();
            }
            else if (effect.TypeSelectTarget == TypeSelectTarget.Player)
            {
                CharacterSelectTarget();
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

        public override void SelectTargetBuilding(Building building)
        {
            DisableStateTarget();

            // Building have random defend. Count random

            if (defendData.AttributeHaveDefend(building.GetTypeBuilding))
            {
                if (defendData.AttributeHaveRandomDefend(building.GetTypeBuilding))
                {
                    if(BoxController.GetController<RandomController>().CountRandom(defendData.GetValueRandomDefend(building.GetTypeBuilding)))
                    {
                        defendData.LoseDefend(building.GetTypeBuilding);

                        EndApply();
                    }
                    else
                    {
                        targetAttributes.Add(building.GetTypeBuilding);

                        ApplyDamage();
                    }
                }
                else
                {
                    defendData.LoseDefend(building.GetTypeBuilding);

                    EndApply();
                }
            }
            else
            {
                targetAttributes.Add(building.GetTypeBuilding);

                ApplyDamage();
            }
        }

        private void GameSelectTarget()
        {
            foreach (var type in effect.TypeTargetObject)
            {
                targetAttributes.Add(type);
            }

            ApplyDamage();
        }

        private void CharacterSelectTarget()
        {
            if (isPlayer)
            {
                targetBuildings = new List<Building>(ObjectsOnScene.Instance.GetBuildingsStorage.GetEnemyBuildings);
            }
            else
            {
                targetBuildings = new List<Building>(ObjectsOnScene.Instance.GetBuildingsStorage.GetPlayerBuildings);
            }

            // Show buildings which can attack

            if (targetBuildings.Count > 0)
            {
                foreach (var building in targetBuildings)
                {
                    if (building != null)
                    {
                        building.EnableStateTarget();
                    }
                }
            }
            else
            {
                UIManager.Instance.GetWindow<InfoWindow>().ShowText("Все здания под защитой. Не получится атаковать.");
            }
        }

        private void ApplyDamage()
        {
            int damage = effect.BaseValue;               

            if (effect.IsNeedAttribute)
            {
                damage += (int)(attackData.GetValueAttribute(effect.TypeAttribute) / 100f * effect.ValueAttribute);
            }

            foreach (var targetAttribute in targetAttributes)
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
            if (targetBuildings.Count > 0)
            {
                foreach (var buildingAnim in targetBuildings)
                {
                    buildingAnim.DisableStateTarget();
                }
            }
        }

        public override void StopApplyEffect()
        {
            DisableStateTarget();
        }
    }
}