using Cards;
using UnityEngine;

namespace EnemyAI
{
    [CreateAssetMenu(fileName = "PriorityCardAi", menuName = "Data/PriorityCardAi")]
    public class SCRO_PriorityCardAi : ScriptableObject
    {
        public SCRO_CardFight[] AttackCardsPriority;
    }
}