using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class HolderBuffAttributeCards : MonoBehaviour
    {
        [SerializeField] private GameObject[] positions;

        private Dictionary<GameObject, bool> checkEmptyPosition = new Dictionary<GameObject, bool>();

        private void Awake()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                checkEmptyPosition.Add(positions[i], true);
            }
        }

        public void AddCard(GameObject card)
        {
            //if()
        }
    }
}