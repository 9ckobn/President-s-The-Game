using UnityEngine;

namespace Cards
{
    public class CardDataBase : MonoBehaviour
    {
        public string ID { get; private set; }

        public CardDataBase(string id)
        {
            ID = id;
        }
    }
}