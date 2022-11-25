using Core;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class AttributesPanel : MonoBehaviour
    {
        [SerializeField] private Text[] labelTexts, valueTexts;

        public void RedrawData(AttributeTextData[] textsData)
        {
            foreach (var text in labelTexts)
            {
                text.gameObject.SetActive(false);
            }

            foreach (var text in valueTexts)
            {
                text.gameObject.SetActive(false);
            }

            if (labelTexts.Length < textsData.Length)
            {
                LogManager.LogError($"Not enougth texts in AttributesPanel! Need {textsData.Length}");
            }

            for (int i = 0; i < textsData.Length; i++)
            {
                labelTexts[i].text = textsData[i].LabelText;
                valueTexts[i].text = textsData[i].ValueText;
                valueTexts[i].color = textsData[i].Color;

                labelTexts[i].gameObject.SetActive(true);
                valueTexts[i].gameObject.SetActive(true);
            }
        }
    }
}