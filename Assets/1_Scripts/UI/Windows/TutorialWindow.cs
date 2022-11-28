using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class TutorialWindow : Window
    {
        [HideInInspector]
        public UnityEvent OnClick;

        [SerializeField] private GameObject popup;
        [SerializeField] private Text tutorText;

        private GameObject imageObject;

        protected override void AfterInitialization()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                OnClick?.Invoke();
            });

            imageObject = GetComponent<Image>().gameObject;
        }

        protected override void BeforeShow()
        {
            imageObject.SetActive(true);
        }

        protected override void BeforeHide()
        {
            imageObject.SetActive(false);
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