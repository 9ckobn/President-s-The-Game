using Core;
using Gameplay;
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
        DataBaseManager.Instance.SelectDeck(0);

        SceneControllers.OnInit += StartGame;
        SceneControllers.InitControllers();
    }

    private void StartGame()
    {
        SceneControllers.OnInit -= StartGame;

        Debug.Log("<color=red>Delete init scene!</color>");

        BoxController.GetController<FightSceneController>().StartGame();
    }
}