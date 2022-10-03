using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject _TextClimate;
    public GameObject _TextLocation;
    public string GeneralLocation = "Соединенные штаты Америки";

    // Start is called before the first frame update
    void Start()
    {
        //_TextClimate.GetComponent<Text>().text = "" + GeneralClimate;
        _TextLocation.GetComponent<Text>().text = "" + GeneralLocation;
        //DataHolder._GeneralClimate = GeneralClimate;
        DataHolder._GeneralLocation = GeneralLocation; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
