using Core;
using System.Collections;
using UI;
using UnityEngine;

public class DELETE_InitDeckScene : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.OnInitialize();
        UIManager.Instance.OnStart();

        DataBaseManager.Instance.OnInit.AddListener(StartGame);
        DataBaseManager.Instance.Initialize();
    }

    private void StartGame() 
    {
        DataBaseManager.Instance.OnInit.RemoveListener(StartGame);
        SceneControllers.Instance.InitControllers();

        StartCoroutine(CoStartGame());
    }

    private IEnumerator CoStartGame()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("<color=red>Delete init scene!</color>");

        UIManager.Instance.ShowWindow<DeckBuildWindow>();
    }
}
