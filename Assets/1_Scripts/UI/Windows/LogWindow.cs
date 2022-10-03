using TMPro;
using UnityEngine;

namespace UI
{
    public class LogWindow : Window
    {
        [SerializeField] private TextMeshProUGUI logText;
        [SerializeField] private TextMeshProUGUI logErrorText;

        public void Log(string text)
        {
            logText.text = text;
        }

        public void LogError(string text)
        {
            logErrorText.text = text;
        }
    }
}