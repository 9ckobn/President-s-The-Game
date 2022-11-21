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

            UIManager.GetWindow<SelectBuffAttributeWindow>().PutCardInSelectedParent(card);
            UIManager.GetWindow<SelectBuffAttributeWindow>().ShowDataAttributes();
        }

        public void DeselecCard(BuffAttributeCardPresidentUI card)
        {
            if (currentCard == card)
            {
                if(currentAttribute == null)
                {
                    UIManager.GetWindow<SelectBuffAttributeWindow>().PutCardInCardsParent(card);
                }
                else
                {
                    if(currentAttribute.CardPresident != null)
                    {
                        UIManager.GetWindow<SelectBuffAttributeWindow>().PutCardInCardsParent(currentAttribute.CardPresident);
                        currentAttribute.RemoveCard();
                    }

                    currentAttribute.PutCardInAttribute(card);
                }

                currentAttribute = null;
                currentCard = null;
            }
        }

        public void SeletAttribute(SelectedAttribute attribute)
        {
            if (currentCard != null)
            {
                currentAttribute = attribute;
                attribute.EnableHighlight();                
            }
        }

        public void DeselectAttribute(SelectedAttribute attribute)
        {
            if (currentAttribute == attribute)
            {
                currentAttribute.DisableHighlight();
                currentAttribute = null;
            }
        }
    }
}
