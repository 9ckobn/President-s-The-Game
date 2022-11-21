using Core;
using UI;
using UnityEngine;


namespace Cards
{
    [CreateAssetMenu(fileName = "BuffAttributePresidentController", menuName = "Controllers/Gameplay/BuffAttributePresidentController")]
    public class BuffAttributePresidentController : BaseController
    {
        private BuffAttributeCardPresidentUI currentCard;

        public void SelectCard(BuffAttributeCardPresidentUI card)
        {
            currentCard = card;

            UIManager.GetWindow<SelectBuffAttributeWindow>().ChangeParentSelectedCard(card.gameObject);
            UIManager.GetWindow<SelectBuffAttributeWindow>().ShowDataAttributes();
        }
    }
}
