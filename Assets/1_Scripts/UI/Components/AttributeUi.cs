using DG.Tweening;
using EffectSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class AttributeUi : MonoBehaviour
    {
        [SerializeField] private TypeAttribute typeAttribute;
        [SerializeField] private Text valueText;

        public TypeAttribute GetTypeAttribute { get => typeAttribute; }

        public void ChangeValue(int value)
        {
            valueText.text = value.ToString();

            //Sequence sequence = DOTween.Sequence();

            //sequence.Append(valueText.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f))
            //    .AppendInterval(0.3f)
            //    .Append(valueText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
        }
    }
}