using Core;
using UnityEngine;

namespace Cards.Storage
{
    [CreateAssetMenu(fileName = "StorageCardImages", menuName = "Data/CardImage/StorageCardImages")]
    public class StorageCardImages : BaseController
    {
        public CardImage[] PresidentImages;

        public Sprite GetPresidentSprite(string id)
        {
            foreach (var president in PresidentImages)
            {
                if(president.ID == id)
                {
                    return president.Sprite;
                }
            }

            BoxController.GetController<LogController>().LogError($"Not have CardImage president with id {id}");

            return null;
        }
    }
}