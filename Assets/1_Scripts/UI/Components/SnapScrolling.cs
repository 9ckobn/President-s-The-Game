using Cards;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class SnapScrolling : MonoBehaviour
    {
        [BoxGroup("Parameters")]
        [SerializeField] private int cardOffset;
        [BoxGroup("Parameters")]
        [SerializeField] private float snapSpeed, scaleOffset, scaleSpeed;

        [BoxGroup("Sroll rect")]
        [SerializeField] private ScrollRect scrollRect;

        private CardBase[] cards;
        private Vector2[] pansPos;
        private Vector2[] pansScale;

        private RectTransform contentRect;
        private Vector2 contentVector;

        private int countCards;
        private int selectedPanID;
        private bool isScrolling;

        private void Start()
        {
            countCards = cards.Length;
            contentRect = GetComponent<RectTransform>();

            pansPos = new Vector2[countCards];
            pansScale = new Vector2[countCards];

            for (int i = 0; i < cards.Length; i++)
            {
                if (i == 0) continue;

                cards[i].transform.localPosition = new Vector2(cards[i - 1].transform.localPosition.x + cards[0].GetComponent<RectTransform>().sizeDelta.x + cardOffset,
                    cards[i].transform.localPosition.y);

                pansPos[i] = -cards[i].transform.localPosition;
            }
        }

        private void FixedUpdate()
        {
            if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
            {
                scrollRect.inertia = false;
            }

            float nearestPos = float.MaxValue;

            for (int i = 0; i < countCards; i++)
            {
                float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
                if (distance < nearestPos)
                {
                    nearestPos = distance;
                    selectedPanID = i;
                }
                float scale = Mathf.Clamp(1 / (distance / cardOffset) * scaleOffset, 0.5f, 1f);
                pansScale[i].x = Mathf.SmoothStep(cards[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                pansScale[i].y = Mathf.SmoothStep(cards[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                cards[i].transform.localScale = pansScale[i];
            }

            float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);

            if (scrollVelocity < 400 && !isScrolling)
            {
                scrollRect.inertia = false;
            }

            if (isScrolling || scrollVelocity > 400)
            {
                return;
            }

            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
        }

        public void Scrolling(bool scroll)
        {
            isScrolling = scroll;

            if (scroll)
            {
                scrollRect.inertia = true;
            }
        }
    }
}