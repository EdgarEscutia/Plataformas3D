using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //void Awake()
    //{

    //}
    //void Start()
    //{
    //    StartCoroutine(MyCoroutine());
    //}
    //IEnumerator MyCoroutine()
    //{
    //    Debug.Log("Starting Coroutine");
    //    yield return null;
    //    Debug.Log("Finishing coroutine");
    //}
    public void Salir()
    {
        Application.Quit();
    }
    public void Play(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void Options(string options)
    {
        SceneManager.LoadScene(options);
    }
    public void Credits(string credits)
    {
        SceneManager.LoadScene(credits);
    }

}
