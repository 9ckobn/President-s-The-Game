using Buildings;
using Core;
using SceneObjects;

namespace EffectSystem
{
    public class ApplyAttackEffect : ApplyEffect
    {
        private AttackEffect effect;

        public override void Apply(Effect currentEffect)
        {
            effect = currentEffect as AttackEffect;

            if (effect.TypeSelectTarget == TypeSelectTarget.Game)
            {
                GameSelectTarget();
            }
            else if (effect.TypeSelectTarget == TypeSelectTarget.Player)
            {
                PlayerSelectTarget();
            }
        }

        public override void SelectTargetBuilding(Building building)
        {
            Building[] buildings = ObjectsOnScene.Instance.GetBuildingsStorage.GetEnemyBuildings;

            foreach (var buildingAnim in buildings)
            {
                buildingAnim.DisableStateTarget();
            }

            ObjectsOnScene.Instance.GetArrowTarget.gameObject.SetActive(false);


        }

        private void GameSelectTarget()
        {

        }

        private void PlayerSelectTarget()
        {
            Building[] buildings = ObjectsOnScene.Instance.GetBuildingsStorage.GetEnemyBuildings;

            foreach (var building in buildings)
            {
                building.EnableStateTarget();
            }

            ObjectsOnScene.Instance.GetArrowTarget.gameObject.SetActive(true);
            ObjectsOnScene.Instance.GetArrowTarget.SetPositions(BoxController.GetController<EffectsController>().GetCurrentCardFight.gameObject, buildings[0].gameObject);
        }
    }
}