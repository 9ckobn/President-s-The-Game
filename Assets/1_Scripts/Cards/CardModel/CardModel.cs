using UnityEngine;

namespace Cards
{
    public class CardModel : CardBase
    {
        [SerializeField] private GameObject parentModel;

        protected GameObject model;

        public GameObject SetMode
        {
            set
            {
                model = value;
                model.transform.SetParent(parentModel.transform);
                model.transform.position = parentModel.transform.position;
            }
        }

        protected override void AfterAwake()
        {
        }
    }
}