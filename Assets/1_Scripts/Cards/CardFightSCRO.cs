using EffectSystem;
using NaughtyAttributes;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "CardFightSCRO", menuName = "Data/Card/CardFightSCRO")]
    public class CardFightSCRO : ScriptableObject
    {
        [BoxGroup("Data")]
        public string ID,Name, Description;
        [BoxGroup("Data")]
        public TypeFactor TypeCost;
        [BoxGroup("Data")]
        public int Cost;

        [BoxGroup("Effects")]
        public SCRO_Effect Effects;
    }
}