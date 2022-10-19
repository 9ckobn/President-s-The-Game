using Core;
using UnityEngine;

namespace Cards.Storage
{
    [CreateAssetMenu(fileName = "StorageCardImages", menuName = "Data/CardImage/StorageCardImages")]
    public class StorageCardImages : BaseController
    {
        public CardImage[] PresidentImages;
        public CardImage[] FightImages;

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
        public Sprite GetFightSprite(string id)
        {
            foreach (var fight in FightImages)
            {
                if (fight.ID == id)
                {
                    return fight.Sprite;
                }
            }

            BoxController.GetController<LogController>().LogError($"Not have CardImage fight with id {id}");

            return null;
        }
    }
}