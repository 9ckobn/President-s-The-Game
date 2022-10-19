using UnityEngine;

namespace Cards.Storage
{
    [CreateAssetMenu(fileName = "StorageCardImages", menuName = "Data/CardImage/StorageCardImages")]
    public class StorageCardImages : ScriptableObject
    {
        public CardImage[] PresidentImages;
        public CardImage[] FightImages;
    }
}