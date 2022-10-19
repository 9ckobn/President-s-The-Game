using UnityEngine;

namespace Cards
{
    public class CardDataBase : MonoBehaviour
    {
        public string ID { get; private set; }
        public Sprite Image { get; private set; }

        public CardDataBase(string id, Sprite image)
        {
            ID = id;
            Image = image;
        }
    }
}