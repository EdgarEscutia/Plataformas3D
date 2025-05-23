using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        { Time.timeScale = 0f; }             
    }
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
