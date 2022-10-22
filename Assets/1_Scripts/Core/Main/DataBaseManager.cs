using Cards;
using Cysharp.Threading.Tasks;
using MoralisUnity;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Platform.Queries;
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
        [HideInInspector]
        public UnityEvent OnInit;

        private bool isUseMoralis;
        private MoralisUser moralisUser;

        private List<CardPresidentDataSerialize> cardsPresidentsData = new List<CardPresidentDataSerialize>();

        public bool SetIsUseMoralis { set => isUseMoralis = value; }
        public List<CardPresidentDataSerialize> GetCardsPresidentData { get => cardsPresidentsData; }

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
            // TODO: Get NFT data

            OnInit?.Invoke();
        }

        public void FakeInitialize()
        {
            LoadDataFromServer();
        }

        private async void LoadDataFromServer()
        {
            using (var httpClient = new HttpClient())
            {
                for (int i = 1; i < 7; i++)
                {
                    var json = await httpClient.GetStringAsync("https://nft.raritygram.io/nfts/presidents/" + i);

                    CardPresidentDataSerialize cardData = JsonUtility.FromJson<CardPresidentDataSerialize>(json);
                    cardsPresidentsData.Add(cardData);
                }
            }

            OnInit?.Invoke();
        }

        public async void ChangeNick(string newNick)
        {
            if (isUseMoralis)
            {
                BoxController.GetController<LogController>().Log($"������ ���");

                moralisUser.username = newNick;

                var result = await moralisUser.SaveAsync();

                if (result)
                {
                    BoxController.GetController<LogController>().Log($"�������� ��� �� {newNick}");
                }
                else
                {
                    BoxController.GetController<LogController>().LogError($"������ ���������� ������ ����!");
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
    }
}