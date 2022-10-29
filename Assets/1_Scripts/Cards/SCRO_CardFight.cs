using EffectSystem;
using EffectSystem.SCRO;
using NaughtyAttributes;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "CardFightSCRO", menuName = "Data/Card/CardFightSCRO")]
    public class SCRO_CardFight : ScriptableObject
    {
        [BoxGroup("Data")]
        public string Id,Name, Description;

        [BoxGroup("Sprite")]
        public Sprite Sprite;

        [BoxGroup("Model")]
        public GameObject Model;

        [BoxGroup("Data")]
        public TypeAttribute[] TypeCost;

        [BoxGroup("Data")]
        public int Cost;

        [BoxGroup("Effects")]
        public SCRO_Effect[] Effects;
    }
}