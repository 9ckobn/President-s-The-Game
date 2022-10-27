using Cards.Data;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.View
{
    public class CardFightView : CardViewBase
    {
        [BoxGroup("Texts")]
        [SerializeField] private Text nameText, descriptionText, costText;

        public void SetData(CardFightData data)
        {
            Debug.Log($"nameText = {nameText}");
            Debug.Log($"data = {data}");
            nameText.text = data.Name;
            descriptionText.text = data.Description;
            costText.text = data.Cost.ToString();
        }
    }
}