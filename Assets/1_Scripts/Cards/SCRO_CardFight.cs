using EffectSystem;
using NaughtyAttributes;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "CardFightSCRO", menuName = "Data/Card/CardFightSCRO")]
    public class SCRO_CardFight : ScriptableObject
    {
        [BoxGroup("Data")]
        public string ID,Name, Description;

        [BoxGroup("Sprite")]
        public Sprite Sprite;

        [BoxGroup("Data")]
        public TypeAttribute[] TypeCost;

        [BoxGroup("Data")]
        public int Cost;

        [BoxGroup("Effects")]
        public SCRO_Effect[] Effects;
    }
}