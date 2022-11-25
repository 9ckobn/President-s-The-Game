using Cards.Data;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Cards.View
{
    public class CardFightView : CardViewBase
    {
        [BoxGroup("Texts")]
        [SerializeField] private TextMeshProUGUI nameText, descriptionText, costText;

        public void SetData(CardFightData data)
        {
            nameText.text = data.Name;
            descriptionText.text = data.Description;
            costText.text = data.Cost.ToString();
        }
    }
}