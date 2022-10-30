using Core;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class AttributesPanel : MonoBehaviour
    {
        [SerializeField] private Text[] attributeTexts;

        public void RedrawData(AttributeTextData[] textsData)
        {
            foreach (var text in attributeTexts)
            {
                text.gameObject.SetActive(false);
            }

            if (attributeTexts.Length < textsData.Length)
            {
                BoxController.GetController<LogController>().LogError($"Not enougth texts in AttributesPanel! Need {textsData.Length}");
            }

            for (int i = 0; i < textsData.Length; i++)
            {
                attributeTexts[i].text = textsData[i].Text;
                attributeTexts[i].color = textsData[i].Color;

                attributeTexts[i].gameObject.SetActive(true);
            }
        }
    }
}