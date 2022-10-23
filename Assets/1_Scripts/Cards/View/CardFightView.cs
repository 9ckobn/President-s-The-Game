using Cards.Data;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.View
{
    public class CardFightView : CardViewBase
    {
        [BoxGroup("Texts")]
        [SerializeField] private Text costText, descriptionText;

        public void SetData(CardFightData data)
        {
        }
    }
}