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
        public string Id, Name, Description;

        [BoxGroup("Sprite")]
        public Sprite Sprite;

        [BoxGroup("Model")]
        public GameObject Model;

        [BoxGroup("Data")]
        public TypeAttribute[] TypeCost;

        [BoxGroup("Data")]
        public int Cost;

        [BoxGroup("Data")]
        public int MaxCountInDeck = 2;

        [BoxGroup("Effects")]
        public SCRO_Effect[] Effects;

        [BoxGroup("Time")]
        [Label("����� ����������� �����")]
        public int Reloading = 1; // 0 - ����� ������������ ��������� ��� �� ���, 1 - ����� ������������ ������ ���, 2 - ����� ���� ���
    }
}