using Buildings;
using Core;
using Data;
using Gameplay;
using SceneObjects;

namespace EffectSystem
{
    public class ApplyAttackEffect : ApplyEffect
    {
        private AttackEffect effect;

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
            targetAttributes.Add(building.GetTypeBuilding);

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

            //ObjectsOnScene.Instance.GetArrowTarget.gameObject.SetActive(false);

            Apply();
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
            Building[] buildings;
            if (isPlayer)
            {
                buildings= ObjectsOnScene.Instance.GetBuildingsStorage.GetEnemyBuildings;
            }
            else
            {
                buildings = ObjectsOnScene.Instance.GetBuildingsStorage.GetPlayerBuildings;
            }

            foreach (var building in buildings)
            {
                building.EnableStateTarget();
            }

            //ObjectsOnScene.Instance.GetArrowTarget.gameObject.SetActive(true);
            //ObjectsOnScene.Instance.GetArrowTarget.SetPositions(BoxController.GetController<EffectsController>().GetCurrentCardFight.gameObject, buildings[0].gameObject);
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
                damage += attackData.GetValueAttribute(effect.TypeAttribute) / 100 * effect.ValueAttribute;
            }

            foreach (var targetAttribute in targetAttributes)
            {
                defendData.DownAttribute(targetAttribute, damage);
                defendData.ShowDamage(targetAttribute);
            }

            EndApply();
        }
    }
}