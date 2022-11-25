using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.Container
{
    public class ContainerCards : MonoBehaviour
    {
        [SerializeField] protected GameObject[] positions;

        private int maxCards, countCards = 0;

        private List<CardBase> cards = new List<CardBase>();

        public int SetMaxCards { set => maxCards = value; }

        public void AddCard(CardBase card)
        {
            if (countCards + 1 > maxCards)
            {
                LogManager.LogError($"Max cards {maxCards}. Can not add card!");
            }
            else
            {
                cards.Add(card);
                card.transform.SetParent(gameObject.transform);
                card.transform.position = positions[countCards].transform.position;
                card.transform.rotation = positions[countCards].transform.rotation;

                countCards++;
            }
        }
    }
}