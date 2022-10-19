using UnityEngine;

namespace Cards.Storage
{
    [CreateAssetMenu(fileName = "CardImage", menuName = "Data/CardImage/CardImage")]
    public class CardImage : ScriptableObject
    {
        public string ID;
        public Sprite Sprite;
    }
}