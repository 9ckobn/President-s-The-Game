using Cards.Data;
using Cards.View;
using NaughtyAttributes;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardPresidentView))]
    public class CardPresidentModel : CardModel
    {
        [BoxGroup("Highlight")]
        [SerializeField] private GameObject highlight;

        public CardPresidentData SetCardData 
        {
            set
            { 
                data = value;

                (view as CardPresidentView).SetData(data as CardPresidentData);
            }
        }

        protected override void MouseEnter()
        {
            throw new System.NotImplementedException();
        }

        protected override void MouseExit()
        {
            throw new System.NotImplementedException();
        }

        protected override void MouseDown()
        {
        }

        public void ChangeHighlight(bool isActive)
        {
            highlight.gameObject.SetActive(isActive);
        }
    }
}