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

        [SerializeField] private GameObject popup, centerPosition, leftPosition;
        [SerializeField] private Text tutorText;

        private GameObject imageObject;
        private PositionTutorPopup positionPopup = PositionTutorPopup.Center;

        public PositionTutorPopup SetPositionPopup { set => positionPopup = value; }

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

                if (positionPopup == PositionTutorPopup.Center)
                {
                    Vector3 pos = centerPosition.transform.position;
                    pos.x += 1500;
                    popup.transform.position = pos;

                    popup.transform.DOMove(centerPosition.transform.position, 0.5f);
                }
                else if (positionPopup == PositionTutorPopup.Left)
                {
                    Vector3 pos = leftPosition.transform.position;
                    pos.x -= 500;
                    popup.transform.position = pos;

                    popup.transform.DOMove(leftPosition.transform.position, 0.5f);
                }
            }
        }
    }

    public enum PositionTutorPopup
    {
        Center,
        Left
    }
}