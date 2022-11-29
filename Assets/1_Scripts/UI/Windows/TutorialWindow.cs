using DG.Tweening;
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

        [SerializeField] private GameObject popup, positionPopup;
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

        protected override void AfterHide()
        {
            popup.SetActive(false);
        }

        public void ShowTutorText(string text)
        {
            tutorText.text = text;

            if (!popup.gameObject.activeSelf)
            {
                popup.gameObject.SetActive(true);

                Vector3 pos = positionPopup.transform.position;
                pos.y += 600;
                popup.transform.position = pos;

                popup.transform.DOMove(positionPopup.transform.position, 0.5f);
            }
        }

        public void HidePopup()
        {
            Vector3 pos = popup.transform.position;
            pos.y += 600;

            popup.transform.DOMove(pos, 0.3f)
                .OnComplete(() =>
                {
                    popup.gameObject.SetActive(false);
                    Hide();
                });
        }
    }
}