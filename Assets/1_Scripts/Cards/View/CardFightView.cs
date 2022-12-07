using Cards.Data;
using NaughtyAttributes;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.View
{
    public class CardFightView : CardViewBase
    {
        [BoxGroup("Texts")]
        [SerializeField] private TextMeshProUGUI nameText, descriptionText, costText;
        [BoxGroup("Highlight image")]
        [SerializeField] private Image highlightRandomImage;
        [BoxGroup("Highlight image")]
        [SerializeField] private Sprite greenHighlightSprite, redHighlightSprite, whiteHighlightSprite, blackHighlightSprite;

        public void SetData(CardFightData data)
        {
            nameText.text = data.Name;
            descriptionText.text = data.Description;
            costText.text = data.Cost.ToString();
        }

        public void ShowWhiteHighlight()
        {
            highlightRandomImage.sprite = whiteHighlightSprite;
            highlightRandomImage.gameObject.SetActive(true);
        }

        public void ShowBlackHighlight()
        {
            highlightRandomImage.sprite = blackHighlightSprite;
            highlightRandomImage.gameObject.SetActive(true);
        }

        public void ShowHighlightRandom(bool luck)
        {
            if (luck)
            {
                highlightRandomImage.sprite = greenHighlightSprite;
            }
            else
            {
                highlightRandomImage.sprite = redHighlightSprite;
            }

            highlightRandomImage.gameObject.SetActive(true);
        }

        public void HideHighlight()
        {
            highlightRandomImage.gameObject.SetActive(false);
        }
    }
}