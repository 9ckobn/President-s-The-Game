using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    private void Start()
    {
        AppManager.Instance.LoadStartScene();
    }
}
