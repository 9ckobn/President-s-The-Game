using UI;

namespace Core
{
    public class StarterFightScene : StarterScene
    {
        protected override void AfterInitControllers()
        {
            UIManager.ShowWindow<SelectBuffAttributeWindow>();
        }
    }
}