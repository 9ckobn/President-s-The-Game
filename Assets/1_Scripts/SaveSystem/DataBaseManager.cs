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
        private const string PARTH_PRESIDENTS = "https://nft.raritygram.io/nfts/presidents/", PATH_LOCAL_DECK_DATA = "DeckData.json"; // C:\Users\unity\AppData\LocalLow\DefaultCompany

        [HideInInspector]
        public UnityEvent OnInit;

        private bool isUseMoralis;
        private MoralisUser moralisUser;

        private List<CardPresidentDataSerialize> cardsPresidentsData = new List<CardPresidentDataSerialize>();
        private List<string> cardsFightID = new List<string>();
        private List<DeckData> decksData = new List<DeckData>();

        public List<CardPresidentDataSerialize> GetCardsPresidentData { get => cardsPresidentsData; }
        public List<string> GetCardsFightID { get => cardsFightID; }
        public List<DeckData> GetDecksData { get => decksData; }

        public bool SetIsUseMoralis { set => isUseMoralis = value; }

        public MoralisUser SetMoralisUser
        {
            set
            {
                moralisUser = value;
                UpdateDataUser();
            }
        }

        #region INITIALIZE

        public void Initialize()
        {
            LoadDataFromServer();
        }

        private async void LoadDataFromServer()
        {
            List<int> idPresidents = new List<int>();

            if (isUseMoralis)
            {
                // TODO: Get id presidents card data from base Moralis
            }
            else
            {
                // Create Fake id presidents 

                for (int i = 1; i < 7; i++)
                {
                    idPresidents.Add(i);
                }
            }

            // Get data presidents from base
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
                // TODO: Get id fight cards from server

            }
            else
            {
                // Create Fake id fights cards 

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
            }

            if (isUseMoralis)
            {
                // TODO: Get deka data from base Moralis

            }
            else
            {
                // Load deck data from json
                try
                {
                    AllDecksDataJson deckDataJson;
                    Debug.Log(Application.persistentDataPath + PATH_LOCAL_DECK_DATA);
                    if (File.Exists(Application.persistentDataPath + PATH_LOCAL_DECK_DATA))
                    {
                        string strLoadJson = File.ReadAllText(Application.persistentDataPath + PATH_LOCAL_DECK_DATA);
                        deckDataJson = JsonUtility.FromJson<AllDecksDataJson>(strLoadJson);

                        foreach (var deckJson in deckDataJson.Decks)
                        {
                            DeckData deck = new DeckData(deckJson.Id, deckJson.NameDeck, deckJson.IdPresidentCards, deckJson.IdFightCards);
                            decksData.Add(deck);
                        }
                    }
                    else
                    {
                        // TODO: Error LogController because init logController after this comit

                        Debug.Log($"Not have file save");
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error load file save - {ex}");
                }
            }

            OnInit?.Invoke();
        }

        #endregion

        public void SaveDecksData()
        {
            if (isUseMoralis)
            {
                // TODO: save data in moralis base
            }
            else
            {
                // Save data on disk

                List<DeckData> decks = BoxController.GetController<DeckBuildController>().GetAllDecks;
                AllDecksDataJson decksData = new AllDecksDataJson();
                decksData.Decks = new DeckDataJson[decks.Count];

                for (int d = 0; d < decks.Count; d++)
                {
                    DeckDataJson deckJson = new DeckDataJson();
                    List<string> idPresidentsCards = new List<string>();
                    List<string> idFightCards = new List<string>();

                    for (int i = 0; i < decks[d].PresidentsId.Count; i++)
                    {
                        idPresidentsCards.Add(decks[d].PresidentsId[i]);
                    }

                    for (int i = 0; i < decks[d].FightsId.Count; i++)
                    {
                        idFightCards.Add(decks[d].FightsId[i]);
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
                    BoxController.GetController<LogController>().LogError($"Error save Deck data - {ex}");
                }
            }
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