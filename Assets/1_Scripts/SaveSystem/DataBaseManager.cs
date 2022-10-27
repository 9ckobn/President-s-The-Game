using Cards;
using Cards.Data;
using Cysharp.Threading.Tasks;
using Gameplay;
using MoralisUnity;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Platform.Queries;
using SaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class DataBaseManager : Singleton<DataBaseManager>
    {
        private const string PARTH_PRESIDENTS = "https://nft.raritygram.io/nfts/presidents/", PATH_LOCAL_DECK_DATA = "DeckData.json";

        [HideInInspector]
        public UnityEvent OnInit;

        private bool isUseMoralis;
        private MoralisUser moralisUser;

        private List<CardPresidentDataSerialize> cardsPresidentsData = new List<CardPresidentDataSerialize>();
        private List<string> cardsFightID = new List<string>();

        public List<CardPresidentDataSerialize> GetCardsPresidentData { get => cardsPresidentsData; }
        public List<string> GetCardsFightID { get => cardsFightID; }

        public bool SetIsUseMoralis { set => isUseMoralis = value; }

        public MoralisUser SetMoralisUser
        {
            set
            {
                moralisUser = value;
                UpdateDataUser();
            }
        }

        public void Initialize()
        {
            List<int> idPresidents = new List<int>();

            for (int i = 1; i < 7; i++)
            {
                idPresidents.Add(i);
            }

            cardsFightID.Add("AirStrike");
            cardsFightID.Add("BountifulHarvest");
            cardsFightID.Add("CustomsReform");
            cardsFightID.Add("DiplomaticImmunity");
            cardsFightID.Add("EducationalInfrastructure");
            cardsFightID.Add("Elections");
            cardsFightID.Add("IntelligenceData");
            cardsFightID.Add("Isolation");
            cardsFightID.Add("JoiningUnion");
            cardsFightID.Add("MilitaryPosition");
            cardsFightID.Add("Patronage");
            cardsFightID.Add("PestControl");
            cardsFightID.Add("StrategicLoan ");
            cardsFightID.Add("Sunction");
            cardsFightID.Add("TechnologicalBreakthrough");

            if (isUseMoralis)
            {
                // TODO: Get id presidents card data from base Moralis                

                // TODO: Get id fight cards from server

                LoadPresidentDataFromServer(idPresidents);
            }
            else
            {
                LoadPresidentDataFromServer(idPresidents);
            }
        }

        public void SaveDecksData()
        {
            List<DeckData> decks = BoxController.GetController<DeckBuildController>().GetAllDecks;
            AllDecksDataJson decksData = new AllDecksDataJson();
            decksData.Decks = new DeckDataJson[decks.Count];

            for (int d = 0; d < decks.Count; d++)
            {
                DeckDataJson deckJson = new DeckDataJson();
                string[] idPresidentsCards = new string[decks[d].PresidentsData.Count];
                string[] idFightCards = new string[decks[d].FightsData.Count];

                for (int i = 0; i < decks[d].PresidentsData.Count; i++)
                {
                    idPresidentsCards[i] = decks[d].PresidentsData[i].ID;
                }

                for (int i = 0; i < decks[d].FightsData.Count; i++)
                {
                    idFightCards[i] = decks[d].FightsData[i].ID;
                }

                deckJson.NameDeck = decks[d].Name;
                deckJson.IdPresidentCards = idPresidentsCards;
                deckJson.IdFightCards = idFightCards;

                decksData.Decks[d] = deckJson;
            }

            string jsonString = JsonUtility.ToJson(decksData);

            try
            {
                File.WriteAllText(Application.persistentDataPath + PATH_LOCAL_DECK_DATA, jsonString);
            }
            catch (Exception ex)
            {
                BoxController.GetController<LogController>().LogError($"Не удалось сохранить игру - {ex}");
            }
        }

        private async void LoadPresidentDataFromServer(List<int> idPresidents)
        {
            using (var httpClient = new HttpClient())
            {
                for (int i = 0; i < idPresidents.Count; i++)
                {
                    var json = await httpClient.GetStringAsync(PARTH_PRESIDENTS + idPresidents[i]);

                    CardPresidentDataSerialize cardData = JsonUtility.FromJson<CardPresidentDataSerialize>(json);
                    cardsPresidentsData.Add(cardData);
                }
            }

            if (isUseMoralis)
            {
                // TODO: Get deka data  from base Moralis

            }
            else
            {

            }

            OnInit?.Invoke();
        }

        #region OLD

        public async void ChangeNick(string newNick)
        {
            if (isUseMoralis)
            {
                BoxController.GetController<LogController>().Log($"Меняем Ник");

                moralisUser.username = newNick;

                var result = await moralisUser.SaveAsync();

                if (result)
                {
                    BoxController.GetController<LogController>().Log($"Изменили имя на {newNick}");
                }
                else
                {
                    BoxController.GetController<LogController>().LogError($"Ошибка сохранения нового ника!");
                }
            }
        }

        private void UpdateDataUser()
        {
            //PersonDataManager dataManager = PersonDataManager.Instance;

            //if (isUseMoralis)
            //{
            //    dataManager.NickUser = moralisUser.username;
            //}
            //else
            //{
            //    dataManager.NickUser = "Player " + Random.Range(0, 1000);
            //}

            //if (isUseMoralis)
            //{
            //    dataManager.KeyUser = moralisUser.ethAddress;
            //}
            //else
            //{
            //    dataManager.KeyUser = "101101101101101";
            //}
        }

        public async void GetTestData()
        {
            if (isUseMoralis)
            {
                //MoralisQuery<TestMoralisObject> query = await Moralis.GetClient().Query<TestMoralisObject>();
                //IEnumerable<TestMoralisObject> getData = await query.FindAsync();

                //var testObjects = getData.ToList();

                //Debug.Log($"COUNT TEST OBJECT = {testObjects.Count}");

                //if (!testObjects.Any())
                //    return;


                //foreach (var testObj in testObjects)
                //{
                //    Debug.Log($"string = {testObj.StringTest} int = {testObj.IntTest}");
                //}
            }
        }

        public async void CreateTestObjects()
        {
            //TestMoralisObject testObject = Moralis.GetClient().Create<TestMoralisObject>();
            //testObject.StringTest = "Text " + Random.Range(100, 1000);
            //testObject.IntTest = Random.Range(100, 1000);

            //var result = await testObject.SaveAsync();

            //if (result)
            //{
            //    Debug.Log("SAVE TEST OBJECT");
            //}
            //else
            //{
            //    Debug.Log("ERROR   ERROR   SAVE TEST OBJECT");
            //}
        }

        #endregion
    }
}