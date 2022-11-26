using Gameplay;
using UI;

namespace Core
{
    public class StarterFightScene : StarterScene
    {
        protected override void AfterInitControllers()
        {
            if (BoxController.GetController<FightSceneController>().IsTutorNow)
            {
                BoxController.GetController<FightSceneController>().StartGame();
            }
            else
            {
                UIManager.ShowWindow<SelectBuffAttributeWindow>();
            }
        }
    }
}