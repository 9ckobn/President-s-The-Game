namespace UI.Buttons
{
    public class SelectDeckButton : DeckButton
    {
        protected override void OnClickButton()
        {
            UIManager.Instance.GetWindow<MenuWindow>().ClickSelectDeckButton(this);   
        }
    }
}