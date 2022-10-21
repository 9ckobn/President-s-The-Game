using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "CardFightSCRO", menuName = "Data/Card/CardFightSCRO")]
    public class CardFightSCRO : ScriptableObject
    {
        public string ID;
        public string Name;
        public int Cost;
    }
}