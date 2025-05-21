using UnityEngine;
using UnityEngine.UI;

public class ChangeGame : MonoBehaviour
{
    public int currentGame;
    
    [SerializeField] Button PreviousBtn;
    [SerializeField] Button NextBtn;


    private void Awake()
    {
        SelectGame(0);
    }
    private void SelectGame(int index)
    {
        PreviousBtn.interactable = (index != 0);
        NextBtn.interactable = (index != transform.childCount-1);
        for (int i = 0; i < transform.childCount; i++) 
        { 
            transform.GetChild(i).gameObject.SetActive(i == index);
        }

    }
    public void GameChange(int change)
    {
        currentGame += change;
        SelectGame(currentGame);
    }
}
