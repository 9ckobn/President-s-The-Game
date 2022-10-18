using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JSONController : MonoBehaviour
{
    //public Factor_Slot DragPresident1;

    [System.Serializable]
    public class Item
    {
        public string id;
        public string name;
        public int level; 
        public string climate;
        public int buff_diplomation;
        public int buff_fortune;
        public int buff_protection;
        public int buff_attack;
        public string factor_materials;
        public int materials_ability_protect;
        public string factor_economic;
        public int economic_ability_protect;
        public int economic_ability_attack;
        public string factor_health;
        public int health_ability_protect;
        public string factor_food;
        public int food_ability_protect;
        public int food_ability_attack;
    }

    [System.Serializable]
    public class ItemList
    {
        public Item[] player;
    }

    public ItemList myItemList = new ItemList(); 

    [ContextMenu("Load")]
    public void LoadField()
    {
        myItemList = JsonUtility.FromJson<ItemList>(File.ReadAllText(Application.streamingAssetsPath + "/JSON_PresidentInfo.json")); // загружаем из JSON
    }

    [ContextMenu("Save")]
    public void SaveField()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_PresidentInfo.json", JsonUtility.ToJson(myItemList)); // сохраняем в JSON 
    }
}









