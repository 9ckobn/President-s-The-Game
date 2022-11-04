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

        private List<Building> targetBuildings;
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
            List<Building> buildingsCharacter;
            randomDefendBuilbinds = new Dictionary<TypeAttribute, RandomDefendEffect>();
            targetBuildings = new List<Building>();

            if (isPlayer)
            {
                buildingsCharacter = new List<Building>(ObjectsOnScene.Instance.GetBuildingsStorage.GetEnemyBuildings);
                defendData = BoxController.GetController<CharactersDataController>().GetEnemyData;
            }
            else
            {
                buildingsCharacter = new List<Building>(ObjectsOnScene.Instance.GetBuildingsStorage.GetPlayerBuildings);
                defendData = BoxController.GetController<CharactersDataController>().GetPlayerData;
            }

            // Check effects defend buildings

            List<Effect> defendEffects = defendData.GetDefendEffects();

            if (defendEffects.Count > 0)
            {
                foreach (var effect in defendEffects)
                {
                    if (effect is RandomDefendEffect)
                    {
                        RandomDefendEffect defendEffect = effect as RandomDefendEffect;

                        for (int i = 0; i < buildingsCharacter.Count; i++)
                        {
                            for (int d = 0; d < defendEffect.TypeDefends.Length; d++)
                            {
                                if (buildingsCharacter[i].GetTypeBuilding == defendEffect.TypeDefends[d])
                                {
                                    int procentRandom = defendData.GetValueAttribute(defendEffect.RandomAttribute);

                                    randomDefendBuilbinds.Add(buildingsCharacter[i].GetTypeBuilding, defendEffect);
                                    buildingsCharacter[i].ShowDefend(procentRandom);
                                }
                            }
                        }
                    }
                    else if (effect is DefendEffect)
                    {
                        DefendEffect defendEffect = effect as DefendEffect;

                        for (int i = 0; i < buildingsCharacter.Count; i++)
                        {
                            for (int d = 0; d < defendEffect.TypeDefends.Length; d++)
                            {
                                if (buildingsCharacter[i].GetTypeBuilding == defendEffect.TypeDefends[d])
                                {
                                    if (defendEffect.ValueDefend == 100)
                                    {
                                        buildingsCharacter[i].ShowDefend();
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
            else
            {
                targetBuildings = buildingsCharacter;
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
            DisableStateTarget();

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
            foreach (var buildingAnim in targetBuildings)
            {
                buildingAnim.DisableStateTarget();
            }
        }

        public override void StopApplyEffect()
        {
            DisableStateTarget();
        }
    }
}