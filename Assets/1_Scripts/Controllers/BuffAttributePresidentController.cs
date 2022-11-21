using Core;
using UI;
using UI.Components;
using UnityEngine;


namespace Cards
{
    [CreateAssetMenu(fileName = "BuffAttributePresidentController", menuName = "Controllers/Gameplay/BuffAttributePresidentController")]
    public class BuffAttributePresidentController : BaseController
    {
        private BuffAttributeCardPresidentUI currentCard = null;
        private SelectedAttribute currentAttribute = null;

        public void SelectCard(BuffAttributeCardPresidentUI card)
        {
            currentCard = card;

            UIManager.GetWindow<SelectBuffAttributeWindow>().ChangeParentSelectedCard(card.gameObject);
            UIManager.GetWindow<SelectBuffAttributeWindow>().ShowDataAttributes();
        }

        public void SeletAttribute(SelectedAttribute attribute)
        {
            if (currentCard != null)
            {
                currentAttribute = attribute;
                attribute.Highlight();                
            }
        }
    }
}
