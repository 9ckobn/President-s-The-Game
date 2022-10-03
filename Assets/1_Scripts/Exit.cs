using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class Exit : MonoBehaviour
{

    public Canvas _canvasScene;

    // Start is called before the first frame update
    void Start()
    {
        string _winResultText;
        if (DataHolder._winnerHolder)
        {
            _winResultText = "Winner";
            _canvasScene.transform.Find("ResultPanel").transform.Find("tokenScope").transform.GetComponent<Text>().text = DataHolder._moralePresidentHolder + " rating points";
        }
        else _winResultText = "Loser";
        _canvasScene.transform.Find("ResultPanel").transform.Find("tokenHead").transform.GetComponent<Text>().text = _winResultText; 
    }

    public void Quit()
    {
        Application.Quit();
    }
}
