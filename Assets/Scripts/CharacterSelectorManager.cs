using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectorManager : MonoBehaviour
{


    [SerializeField] TheLastStarsCS grid;


    [SerializeField] Button startGame;

    public static string confirmedCharacter1;
    public static string confirmedCharacter2;


    string currentScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;

        startGame.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(grid.confirmedCharacter1 != null && grid.confirmedCharacter2 != null)
        {
            startGame.gameObject.SetActive(true);
            startGame.interactable = true;
        }
        else
        {
            startGame.gameObject.SetActive(false);
            startGame.interactable = false;
        }
    }

    public void StartGame()
    {


            confirmedCharacter1 = grid.confirmedCharacter1.name;
            confirmedCharacter2 = grid.confirmedCharacter2.name;


            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void BackScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
