using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class CardPresidentView : CardViewBase
    {
        [BoxGroup("Texts")]
        [SerializeField] private Text level, attack, attackBuff, protection, protectionBuff, fortune, fortuneBuff, diplomation, diplomationBuff;
        [BoxGroup("Info images")]
        [SerializeField] private Image attackImage, protectionImage, fortuneImage, diplomationImage;

        public void SetData(CardPresidentData data, Sprite sprite)
        {
        }
    }
}
