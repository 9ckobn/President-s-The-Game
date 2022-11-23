using Cards.Data;
using Core;
using Data;
using System.Collections.Generic;
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

        private List<DataBuffPresidents> dataBuff = new List<DataBuffPresidents>();
        private List<DataBuffPresidents> viewDataBuff = new List<DataBuffPresidents>();

        public void SelectCard(BuffAttributeCardPresidentUI card)
        {
            currentCard = card;
            CreateViewDataBuff();

            UIManager.GetWindow<SelectBuffAttributeWindow>().PutCardInSelectedParent(card);
            UIManager.GetWindow<SelectBuffAttributeWindow>().ShowDataAttributes(viewDataBuff);
            UIManager.GetWindow<SelectBuffAttributeWindow>().DisableRaycastCards();
        }

        public void DeselecCard(BuffAttributeCardPresidentUI card)
        {
            if (currentCard == card)
            {
                if (currentAttribute == null)
                {
                    UIManager.GetWindow<SelectBuffAttributeWindow>().PutCardInCardsParent(card);
                }
                else
                {
                    currentAttribute.PutCardInAttribute(card);
                    AddBuff(currentAttribute);
                }

                currentAttribute = null;
                currentCard = null;

                UIManager.GetWindow<SelectBuffAttributeWindow>().HideDataAttributes();
            }

            UIManager.GetWindow<SelectBuffAttributeWindow>().EnableRaycastCards();
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

        private void CreateViewDataBuff()
        {
            viewDataBuff = new List<DataBuffPresidents>();
            CardPresidentData data = currentCard.GetData;

            int valueBuff = (int)((data.Attack + data.Defend + data.Luck + data.Diplomatic) * 0.1f);

            foreach (var buff in data.PossiblePresidentBuff)
            {
                viewDataBuff.Add(new DataBuffPresidents(buff, valueBuff));
            }
        }

        private void AddBuff(SelectedAttribute attribute)
        {
            dataBuff.Add(new DataBuffPresidents(attribute.TypeAttribute, attribute.Value));
        }

        public void RemoveAttribute(SelectedAttribute attribute)
        {
            foreach (var buff in dataBuff)
            {
                if (buff.TypeAttribute == attribute.TypeAttribute)
                {
                    dataBuff.Remove(buff);
                    return;
                }
            }
        }
    }
}
