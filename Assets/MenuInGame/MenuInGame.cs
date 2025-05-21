using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuInGame : MonoBehaviour
{
    [SerializeField] GameObject menuInGame;

    private bool options = false;
    private void Awake()
    {
        menuInGame.SetActive(false);
        options = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !options)
        {
            PauseGame();
            options = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && options)
        {
            ReturnGame();
            options = false;
        }
    }

    public void ReturnGame()
    {
        Time.timeScale = 1.0f;
        menuInGame.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        menuInGame.SetActive(true);
    }
    public void SelectScene(string scene)
    { 
        SceneManager.LoadScene(scene);
        Time.timeScale = 1.0f;
    }
}
