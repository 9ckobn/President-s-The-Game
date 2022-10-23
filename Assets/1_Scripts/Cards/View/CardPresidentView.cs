using Cards.Data;
using NaughtyAttributes;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.View
{
    public class CardPresidentView : CardViewBase
    {
        [BoxGroup("Texts")]
        [SerializeField] private Text rarityrankText, nameText;
        [BoxGroup("Attributes")]
        [SerializeField] private AttributePresidentUI attackAttribute, luckAttribute, defendAttribute, diplomaticAttribute;

        public void SetData(CardPresidentData data)
        {
            rarityrankText.text = data.Rarityrank.ToString();
            nameText.text = data.Name;

            attackAttribute.SetData(data.CommonAttack, data.BuffAttack.GetState);
        }
    }
}
