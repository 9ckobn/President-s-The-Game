using System.Collections;
using UI;
using UnityEngine;

public class DELETE_InitScene : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.OnInitialize();
        UIManager.Instance.OnStart();

        StartCoroutine(CoStart());
    }

    private IEnumerator CoStart()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.ShowWindow<DeckBuildWindow>();
    }
}
