using Cards;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class ScrollCards : MoveObject
    {
        private const int CARDS_IN_BLOCK = 6, WIDTH_BLOCK = 990;

        [SerializeField] private BlockCards prefabLineCard;
        [SerializeField] private GameObject contentLinesParent;
        [SerializeField] private Text countBlockText;
        [SerializeField] private Button leftLeafButton, rightLeafButton;
        [SerializeField] private MoveScrollCards moveScrollCards;

        private List<BlockCards> blocksCard = new List<BlockCards>();
        private List<GameObject> cards;

        private int currentBlock = 0;

        private void Awake()
        {
            leftLeafButton.onClick.AddListener(LeaftLeft);
            rightLeafButton.onClick.AddListener(LeadRight);
        }

        public void ClearLines()
        {
            for (int i = blocksCard.Count - 1; i >= 0; i--)
            {
                Destroy(blocksCard[i].gameObject);
            }

            blocksCard.Clear();
            contentLinesParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            moveScrollCards.ResetStartPosition();
            currentBlock = 0;
        }

        public void AddCards(List<CardPresidentUI> cardsPresident)
        {
            cards = new List<GameObject>();

            foreach (var card in cardsPresident)
            {
                cards.Add(card.gameObject);
            }

            AddCards();
            CheckLeafButtons();
        }

        public void AddCards(List<CardFightUI> cardsFight)
        {
            cards = new List<GameObject>();

            foreach (var card in cardsFight)
            {
                cards.Add(card.gameObject);
            }

            AddCards();
            CheckLeafButtons();
        }

        private void AddCards()
        {
            blocksCard = new List<BlockCards>();

            int countCards = 0;
            BlockCards line = null;

            for (int c = 0; c < cards.Count; c++)
            {
                if (countCards == 0)
                {
                    line = Instantiate(prefabLineCard, contentLinesParent.transform);
                    blocksCard.Add(line);

                    float prevWidth = contentLinesParent.GetComponent<RectTransform>().rect.width;
                    contentLinesParent.GetComponent<RectTransform>().sizeDelta = new Vector2(prevWidth + WIDTH_BLOCK, 0);
                }

                cards[c].gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.12f, 1.12f);

                line.AddCard(cards[c].gameObject);

                countCards++;
                if (countCards >= CARDS_IN_BLOCK)
                {
                    countCards = 0;
                }
            }

            RedrawCoutBlockText();
        }

        private void LeaftLeft()
        {
            moveScrollCards.MoveLeft();
            currentBlock--;
            CheckLeafButtons();
            RedrawCoutBlockText();
        }

        private void LeadRight()
        {
            moveScrollCards.MoveRight();
            currentBlock++;
            CheckLeafButtons();
            RedrawCoutBlockText();
        }

        private void CheckLeafButtons()
        {
            leftLeafButton.gameObject.SetActive(false);
            rightLeafButton.gameObject.SetActive(false);

            if (currentBlock > 0)
            {
                leftLeafButton.gameObject.SetActive(true);

                if (currentBlock == blocksCard.Count - 1)
                {
                    rightLeafButton.gameObject.SetActive(false);
                }
                else
                {
                    rightLeafButton.gameObject.SetActive(true);
                }
            }
            else if (currentBlock == 0)
            {
                leftLeafButton.gameObject.SetActive(false);

                if(blocksCard.Count > 1)
                {
                    rightLeafButton.gameObject.SetActive(true);
                }
            }
        }

        private void RedrawCoutBlockText()
        {
            countBlockText.text = $"0{currentBlock + 1}/0{blocksCard.Count}";
        }
    }
}