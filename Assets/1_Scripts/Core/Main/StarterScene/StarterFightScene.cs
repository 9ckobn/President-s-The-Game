using Gameplay;
using UI;

namespace Core
{
    public class StarterFightScene : StarterScene
    {
        protected override void AfterInitControllers()
        {
            BoxController.GetController<FightSceneController>().StartGame();
        }
    }
}