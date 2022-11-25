using UnityEngine;

namespace Data
{
    public class AttributeTextData
    {
        public string LabelText, ValueText;
        public Color Color;

        public AttributeTextData(string labelText, string valueText, Color color)
        {
            LabelText = labelText;
            ValueText = valueText;
            Color = color;
        }
    }
}