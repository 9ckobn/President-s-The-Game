namespace UI.Buttons
{
    public class SelectDeckButton : DeckButton
    {
        protected override void OnClickButton()
        {
            UIManager.GetWindow<MenuWindow>().ClickSelectDeckButton(this);   
        }
    }
}