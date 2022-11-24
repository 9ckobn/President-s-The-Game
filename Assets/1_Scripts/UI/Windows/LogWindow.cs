using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LogWindow : MonoBehaviour
    {
        [SerializeField] private Text logText;
        [SerializeField] private Text logErrorText;

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