using Buildings;
using Core;
using Data;
using Gameplay;
using SceneObjects;
using System.Collections.Generic;

namespace EffectSystem
{
    public class ApplyAttackEffect : ApplyEffect
    {
        private AttackEffect effect;

        private List<Building> targetBuildings, immortalBuilding;
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
            randomDefendBuilbinds = new Dictionary<TypeAttribute, RandomDefendEffect>();
            immortalBuilding = new List<Building>();

            if (isPlayer)
            {
                targetBuildings = new List<Building>(ObjectsOnScene.Instance.GetBuildingsStorage.GetEnemyBuildings);
                defendData = BoxController.GetController<CharactersDataController>().GetEnemyData;
            }
            else
            {
                targetBuildings = new List<Building>(ObjectsOnScene.Instance.GetBuildingsStorage.GetPlayerBuildings);
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

                        for (int i = 0; i < targetBuildings.Count; i++)
                        {
                            for (int d = 0; d < defendEffect.TypeDefends.Length; d++)
                            {
                                if (targetBuildings[i].GetTypeBuilding == defendEffect.TypeDefends[d])
                                {
                                    int procentRandom = defendData.GetValueAttribute(defendEffect.RandomAttribute);

                                    randomDefendBuilbinds.Add(targetBuildings[i].GetTypeBuilding, defendEffect);
                                    targetBuildings[i].ShowDefend(procentRandom);
                                }
                            }
                        }
                    }
                    else if (effect is DefendEffect)
                    {
                        DefendEffect defendEffect = effect as DefendEffect;

                        for (int i = 0; i < targetBuildings.Count; i++)
                        {
                            for (int d = 0; d < defendEffect.TypeDefends.Length; d++)
                            {
                                if (targetBuildings[i].GetTypeBuilding == defendEffect.TypeDefends[d])
                                {
                                    if (defendEffect.ValueDefend == 100)
                                    {
                                        immortalBuilding.Add(targetBuildings[i]);
                                        targetBuildings[i].ShowDefend();
                                        targetBuildings.Remove(targetBuildings[i]);
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

            foreach (var building in targetBuildings)
            {
                if (building != null)
                {
                    building.EnableStateTarget();
                }
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
                damage += attackData.GetValueAttribute(effect.TypeAttribute) / 100 * effect.ValueAttribute;
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
            Building[] buildings;

            if (isPlayer)
            {
                buildings = ObjectsOnScene.Instance.GetBuildingsStorage.GetEnemyBuildings;
            }
            else
            {
                buildings = ObjectsOnScene.Instance.GetBuildingsStorage.GetPlayerBuildings;
            }

            foreach (var buildingAnim in buildings)
            {
                buildingAnim.DisableStateTarget();
            }
        }
    }
}