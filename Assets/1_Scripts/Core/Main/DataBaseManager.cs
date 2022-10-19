using Cards;
using Cysharp.Threading.Tasks;
using MoralisUnity;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Platform.Queries;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private CardsPresidentsList cardsPresident = new CardsPresidentsList();

        public bool SetIsUseMoralis { set => isUseMoralis = value; }
        public CardsPresidentsList GetCardsPresident { get => cardsPresident; }

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
            cardsPresident = JsonUtility.FromJson<CardsPresidentsList>(File.ReadAllText(Application.streamingAssetsPath + "/JSON_PresidentInfo.json"));

            OnInit?.Invoke();
        }

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
    }
}