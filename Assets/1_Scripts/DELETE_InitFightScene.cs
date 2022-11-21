using Core;
using Gameplay;
using UI;
using UnityEngine;

public class DELETE_InitFightScene : MonoBehaviour
{
    private void Start()
    {
        DataBaseManager.OnInit += LoadData;
        DataBaseManager.Instance.Initialize();
    }

    private void LoadData()
    {
        DataBaseManager.OnInit -= LoadData;

        SceneControllers.OnInit += StartGame;
        SceneControllers.InitControllers();
    }

    private void StartGame()
    {
        SceneControllers.OnInit -= StartGame;

        Debug.Log("<color=red>Delete init scene!</color>");

        UIManager.ShowWindow<SelectBuffAttributeWindow>();
        //BoxController.GetController<FightSceneController>().StartGame();
    }
}