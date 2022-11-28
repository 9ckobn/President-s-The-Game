using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class TutorialWindow : Window
    {
        [HideInInspector]
        public UnityEvent OnClick;

        [SerializeField] private GameObject popup;
        [SerializeField] private Text tutorText;

        protected override void AfterInitialization()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                OnClick?.Invoke();
            });
        }

        public void ShowTutorText(string text)
        {
            tutorText.text = text;
            popup.gameObject.SetActive(true);
        }

        public void HidePopup()
        {
            popup.SetActive(false);
        }
    }
}