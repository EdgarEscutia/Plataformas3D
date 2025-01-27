using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenuController : MonoBehaviour
{
    private void Update()
    {
        Time.timeScale = 0f;
    }
    public void MainMenu(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void Salir()
    {
        Application.Quit();
    }
}
