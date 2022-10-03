using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JSONController_Card : MonoBehaviour
{

    [System.Serializable]
    public class ItemCard
    {
        public string id;
        public int cost;
        public int materials;
        public int economic;
        public int health;
        public int food;
        public int attack;
        public int protect;
        public int diplomation;
        public int fortune;
        public int deltamorale_positive;
        public int deltamorale_negative; 
    }

    [System.Serializable]
    public class ItemListCard
    {
        public ItemCard[] fight_card;
    }

    public ItemListCard myListFightCard = new ItemListCard(); 

    [ContextMenu("Load")]
    public void LoadField()
    {
        myListFightCard = JsonUtility.FromJson<ItemListCard>(File.ReadAllText(Application.streamingAssetsPath + "/JSON_FightCardInfo.json")); // загружаем из JSON
    }

    [ContextMenu("Save")]
    public void SaveField()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_FightCardInfo.json", JsonUtility.ToJson(myListFightCard)); // сохраняем в JSON 
    }
}









