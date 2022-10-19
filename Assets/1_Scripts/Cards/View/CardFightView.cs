using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class CardFightView : CardViewBase
    {
        [BoxGroup("Texts")]
        [SerializeField] private Text costText, descriptionText;
    }
}