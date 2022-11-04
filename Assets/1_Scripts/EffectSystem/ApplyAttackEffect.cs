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

        private List<Building> characterDefendBuildings, targetBuildings;
        private Dictionary<TypeAttribute, RandomDefendEffect> randomDefendBuilbinds = new Dictionary<TypeAttribute, RandomDefendEffect>();

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as AttackEffect;
            targetAttributes.Clear();

            if (effect.TypeSelectTarget == TypeSelectTarget.Game)
            {
                GameSelectTarget();
            }
            else if (effect.TypeSelectTarget == TypeSelectTarget.Player)
            {
                CharacterSelectTarget();
            }
        }

        public override void SelectTargetBuilding(Building building)
        {
            DisableStateTarget();

            // Building have random defend. Count random

            if (randomDefendBuilbinds.ContainsKey(building.GetTypeBuilding))
            {
                // Random true
                if (BoxController.GetController<EffectsController>().CherkRandomDefendEffect(randomDefendBuilbinds[building.GetTypeBuilding]))
                {
                    LoseAttack();
                }
                else // Random false
                {
                    targetAttributes.Add(building.GetTypeBuilding);

                    Apply();
                }
            }
            else // Attack building
            {
                targetAttributes.Add(building.GetTypeBuilding);

                Apply();
            }
        }

        private void GameSelectTarget()
        {
            foreach (var type in effect.TypeTargetObject)
            {
                targetAttributes.Add(type);
            }

            Apply();
        }

        private void CharacterSelectTarget()
        {
            CharacterData defendData;

            randomDefendBuilbinds = new Dictionary<TypeAttribute, RandomDefendEffect>();

            if (isPlayer)
            {
                characterDefendBuildings = new List<Building>(ObjectsOnScene.Instance.GetBuildingsStorage.GetEnemyBuildings);
                defendData = BoxController.GetController<CharactersDataController>().GetEnemyData;
            }
            else
            {
                characterDefendBuildings = new List<Building>(ObjectsOnScene.Instance.GetBuildingsStorage.GetPlayerBuildings);
                defendData = BoxController.GetController<CharactersDataController>().GetPlayerData;
            }

            targetBuildings = new List<Building>();
            foreach (var building in characterDefendBuildings)
            {
                targetBuildings.Add(building);
            }

            // Check effects defend buildings

            List<Effect> defendEffects = defendData.GetDefendEffects();

            if (defendEffects.Count > 0)
            {
                foreach (var effect in defendEffects)
                {
                    foreach (var characterBuilding in characterDefendBuildings)
                    {
                        foreach (var typeDefend in (effect as DefendEffect).TypeDefends)
                        {
                            if (characterBuilding.GetTypeBuilding == typeDefend)
                            {
                                if(effect is RandomDefendEffect)
                                {
                                    RandomDefendEffect randomEffect = effect as RandomDefendEffect;

                                    randomDefendBuilbinds.Add(characterBuilding.GetTypeBuilding, randomEffect);
                                    characterBuilding.ShowDefend(randomEffect.ValueDefend);
                                }
                                else
                                {
                                    DefendEffect defendEffect = effect as DefendEffect;

                                    if (defendEffect.ValueDefend == 100)
                                    {
                                        characterBuilding.ShowDefend();
                                        targetBuildings.Remove(characterBuilding);
                                    }
                                    else
                                    {
                                        BoxController.GetController<LogController>().LogError($"Not logic defend < 100% !!!");
                                    }
                                }
                            }
                        }
                    }
                }
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

        private void Apply()
        {
            int damage = effect.BaseValue;
            CharacterData attackData, defendData = null;

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

            if (effect.IsNeedAttribute)
            {
                damage += (int)(attackData.GetValueAttribute(effect.TypeAttribute) / 100f * effect.ValueAttribute);
            }

            foreach (var targetAttribute in targetAttributes)
            {
                defendData.DownAttribute(targetAttribute, damage);
                defendData.ShowDamage(targetAttribute);
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
            if (characterDefendBuildings.Count > 0)
            {
                foreach (var buildingAnim in characterDefendBuildings)
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