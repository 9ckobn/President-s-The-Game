using TMPro;
using UnityEngine;

namespace UI.Components
{
    public class BuildingCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueAttribute, valueDefend;

        public void ShowValueAttribute(int value)
        {
            valueAttribute.text = value.ToString();
        }

        public void ShowValueDefend(int value)
        {
            valueDefend.text = value.ToString();
            valueDefend.gameObject.SetActive(true);
        }

        public void HideValueDefend()
        {
            valueDefend.gameObject.SetActive(false);
        }
    }
}