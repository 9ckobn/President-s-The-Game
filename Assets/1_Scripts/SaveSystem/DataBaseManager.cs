using Cards;
using Cards.Data;
using Cards.DeckBuild;
using Cards.Type;
using SaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Core
{
    public class DataBaseManager : Singleton<DataBaseManager>
    {
        private const string PATH_PRESIDENTS = "https://nft.raritygram.io/nfts/presidents/", PATH_LOCAL_DECK_DATA = "DeckData.json";

        public static event Action OnInit;

        private bool isUseMoralis;

        public List<CardPresidentDataSerialize> CardsPresidentsData { get; private set; }
        public List<DeckData> DecksData { get; private set; }


        /// <summary>
        /// 
        // TODO: Move this fields in GameState
        /// 
        /// </summary>
        public DeckData SelectedDeck
        {
            get
            {
                foreach (var deck in DecksData)
                {
                    if (deck.IsSelected)
                    {
                        return deck;
                    }
                }

                Debug.Log($"<color=red>Not have selected deck!</color>");
                return null;
            }
        }

        public TypeClimate TypeClimate { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// 


        #region INITIALIZE

        public void Initialize()
        {
            LoadDataFromServer();
        }

        private async void LoadDataFromServer()
        {
            List<string> idPresidents = new List<string>();
            CardsPresidentsData = new List<CardPresidentDataSerialize>();
            DecksData = new List<DeckData>();

            //if (isUseMoralis)
            //{
            // TODO: Get id presidents card data from base Moralis

            // TODO: Get deka data from base Moralis

            // TODO: Get id fight cards from server

            //}
            //else
            //{
            // Load data from json

            try
            {
                AllDecksDataJson deckDataJson;

#if UNITY_EDITOR

                if (File.Exists(Application.persistentDataPath + PATH_LOCAL_DECK_DATA))
                {
                    string strLoadJson = File.ReadAllText(Application.persistentDataPath + PATH_LOCAL_DECK_DATA);
                    deckDataJson = JsonUtility.FromJson<AllDecksDataJson>(strLoadJson);
                    DeckData deck = null;

                    foreach (var deckJson in deckDataJson.Decks)
                    {
                        deck = new DeckData(deckJson.Id, deckJson.NameDeck, deckJson.IsComplete, deckJson.IsSelected, deckJson.IdPresidentCards, deckJson.IdFightCards);
                        DecksData.Add(deck);
                    }
                }
                else
                {
                    // TODO: Error LogController because init logController after this comit

                    Debug.Log($"Not have file save");
                }
#endif

                // Create Fake id presidents 
                //for (int i = 1; i < 7; i++)
                //{
                //    idPresidents.Add(i.ToString());
                //}




                // Get data presidents from base
                for (int i = 0; i < idPresidents.Count; i++)
                {
                    string path = PATH_PRESIDENTS + idPresidents[i];

                    var request = UnityWebRequest.Get(path);

                    //await request.SendWebRequest();

                    if (request.result == UnityWebRequest.Result.ConnectionError)
                    {
                        LogManager.LogError($"{request.result}");
                    }

                    if (request.result == UnityWebRequest.Result.ProtocolError)
                    {
                        LogManager.LogError($"Protocol error!");
                    }

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log($"url = {request.downloadHandler.text}");

                        CardPresidentDataSerialize cardData = JsonUtility.FromJson<CardPresidentDataSerialize>(request.downloadHandler.text);
                        CardsPresidentsData.Add(cardData);
                    }
                }

                //for (int i = 1; i < 7; i++)
                //{
                //    string jsonFile = Resources.Load("PresidentsData_" + i).ToString();
                //    CardPresidentDataSerialize cardData = JsonUtility.FromJson<CardPresidentDataSerialize>(jsonFile);

                //    CardsPresidentsData.Add(cardData);
                //}

            }
            catch (Exception ex)
            {
                Debug.Log($"Error load file save - {ex}");
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

                List<DeckData> decks = BoxController.GetController<DeckBuildController>().Decks;
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
                    deckJson.Id = decks[d].Id;
                    deckJson.IdPresidentCards = idPresidentsCards;
                    deckJson.IdFightCards = idFightCards;
                    deckJson.IsComplete = decks[d].IsComplete;
                    deckJson.IsSelected = decks[d].IsSelected;

                    decksData.Decks[d] = deckJson;
                }

                string jsonString = JsonUtility.ToJson(decksData);

                try
                {
                    File.WriteAllText(Application.persistentDataPath + PATH_LOCAL_DECK_DATA, jsonString);
                }
                catch (Exception ex)
                {
                    LogManager.LogError($"Error save Deck data - {ex}");
                }
            }
        }

        #region OLD

        public async void ChangeNick(string newNick)
        {
            if (isUseMoralis)
            {
                LogManager.Log($"?????? ???");

                //moralisUser.username = newNick;

                //var result = await moralisUser.SaveAsync();

                //if (result)
                //{
                //    LogManager.Log($"???????? ??? ?? {newNick}");
                //}
                //else
                //{
                //    LogManager.LogError($"?????? ?????????? ?????? ????!");
                //}
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