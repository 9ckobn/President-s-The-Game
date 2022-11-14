using Cards;
using Cards.Data;
using Cards.Storage;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public abstract class StorageCardsController : BaseController
    {
        [SerializeField] protected StorageCardImages storageImages;
        [SerializeField] protected SCRO_CardFight[] cardFightSCRO;

        public List<CardPresidentData> CardsPresidentData { get; protected set; }
        public List<CardFightData> CardsFightData { get; protected set; }

        public override void OnInitialize()
        {
            CardsPresidentData = new List<CardPresidentData>();
            CardsFightData = new List<CardFightData>();

            AfterInitialize();
        }

        public abstract void AfterInitialize();
    }
}