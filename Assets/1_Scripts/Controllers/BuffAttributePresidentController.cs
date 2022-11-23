using Cards.Data;
using Core;
using Data;
using EffectSystem;
using System.Collections.Generic;
using System.Linq;
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

        private bool isChooseAllBuffs = false;

        public int GetBuffValue(TypeAttribute typeAttribute)
        {
            DataBuffPresidents buff = dataBuff.FirstOrDefault(buff => buff.TypeAttribute == typeAttribute);

            return buff == null ? 0 : buff.Value;
        }

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

            int valueBuff = (int)((data.Attack + data.Defend + data.Luck + data.Diplomatic) * MainData.MULTIPLIER_BUFF);

            foreach (var buff in data.PossiblePresidentBuff)
            {
                viewDataBuff.Add(new DataBuffPresidents(buff, valueBuff));
            }
        }

        private void AddBuff(SelectedAttribute attribute)
        {
            dataBuff.Add(new DataBuffPresidents(attribute.TypeAttribute, attribute.Value));

            CheckCountBuffs();
        }

        public void RemoveAttribute(SelectedAttribute attribute)
        {
            foreach (var buff in dataBuff)
            {
                if (buff.TypeAttribute == attribute.TypeAttribute)
                {
                    dataBuff.Remove(buff);
                    CheckCountBuffs();

                    return;
                }
            }
        }

        private void CheckCountBuffs()
        {
            isChooseAllBuffs = dataBuff.Count == MainData.MAX_PRESIDENT_CARDS;

            if (isChooseAllBuffs)
            {
                UIManager.GetWindow<SelectBuffAttributeWindow>().ShowButtonStartGame();
            }
            else
            {
                UIManager.GetWindow<SelectBuffAttributeWindow>().HideButtonStartGame();
            }
        }
    }
}
