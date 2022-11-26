using Cards;
using NaughtyAttributes;
using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "TutorialData", menuName = "Data/Tutorial/TutorialData")]
    public class SCRO_TutorialData : ScriptableObject
    {
        [BoxGroup("Fight cards")]
        [SerializeField] private SCRO_CardFight[] playerFightCards, enemyFightCards;
        [BoxGroup("President cards")]
        [SerializeField] private int[] playerPresidentCards, enemyPresidentCards;
    }
}