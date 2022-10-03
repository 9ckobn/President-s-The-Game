using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneLoader : MonoBehaviour
{
    private GameObject _window1;
    private GameObject _window2; 
    private GameObject _window3; 
    // Start is called before the first frame update
    void Start()
    {
        _window1 = transform.root.Find("WINDOW_1").gameObject;
        _window2 = transform.root.Find("WINDOW_2").gameObject;
        _window3 = transform.root.Find("WINDOW_3").gameObject;

        _window1.SetActive(true);
        _window2.SetActive(false);
        _window3.SetActive(false);
    }
    
    public void Window2()
    {
        _window1.SetActive(false);
        _window2.SetActive(true);
    }

    public void Window3()
    {
        _window2.SetActive(false);
        _window3.SetActive(true);
    }

    public void SceneLoad(int number)
    {
        SceneManager.LoadScene(number);
    }
}
