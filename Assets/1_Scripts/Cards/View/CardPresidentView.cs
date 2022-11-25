using Cards.Data;
using EffectSystem;
using NaughtyAttributes;
using TMPro;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.View
{
    public class CardPresidentView : CardViewBase
    {
        [BoxGroup("Texts")]
        [SerializeField] private TextMeshProUGUI rarityrankText, nameText;
        [BoxGroup("Attributes")]
        [SerializeField] private AttributePresidentUI attackAttribute, luckAttribute, defendAttribute, diplomaticAttribute;        

        public void SetData(CardPresidentData data)
        {
            rarityrankText.text = data.Rarityrank.ToString();
            nameText.text = data.Name;
            
            attackAttribute.SetData(data.Attack, data.GetBuffAttributeState(TypeAttribute.Attack));
            luckAttribute.SetData(data.Luck, data.GetBuffAttributeState(TypeAttribute.Luck));
            defendAttribute.SetData(data.Defend, data.GetBuffAttributeState(TypeAttribute.Defend));
            diplomaticAttribute.SetData(data.Diplomatic, data.GetBuffAttributeState(TypeAttribute.Diplomacy));
        }
    }
}
