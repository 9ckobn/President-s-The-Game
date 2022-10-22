using UnityEngine;

namespace Cards.Data
{
    public class CardDataBase : MonoBehaviour
    {
        public string ID { get; private set; }
        public Sprite Sprite { get; private set; }

        public CardDataBase(string id, Sprite sprite)
        {
            ID = id;
            Sprite = sprite;
        }
    }
}