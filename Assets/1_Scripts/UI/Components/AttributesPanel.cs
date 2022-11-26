using Core;
using Data;
using UnityEngine;

namespace UI.Components
{
    public class AttributesPanel : MonoBehaviour
    {
        [SerializeField] private AttributeUi[] attributesUi;

        public void RedrawData(AttributeTextData[] textsData)
        {
            if (attributesUi.Length < textsData.Length)
            {
                LogManager.LogError($"Not enougth texts in AttributesPanel! Need {textsData.Length}");
            }

            foreach (var data in textsData)
            {
                foreach (var attribute in attributesUi)
                {
                    if(attribute.GetTypeAttribute == data.TypeAttribute)
                    {
                        attribute.ChangeValue(data.Value);
                        break;
                    }
                }
            }
        }
    }
}