using Cards;
using NaughtyAttributes;
using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "TutorialData", menuName = "Data/Tutorial/TutorialData")]
    public class SCRO_TutorialData : ScriptableObject
    {
        [BoxGroup("Fight cards")]
        [SerializeField] public SCRO_CardFight[] PlayerFightCards, EnemyFightCards;
        [BoxGroup("President cards")]
        [SerializeField] public int[] PlayerPresidentCards, EnemyPresidentCards;
    }
}