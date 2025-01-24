using UnityEngine;
using UnityEngine.UI;

public class GameSelection : MonoBehaviour
{
    private int currentGame;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    private void Awake()
    {
        SelectGame(0);
    }
    private void SelectGame(int index)
    {
        previousButton.interactable = (index != 0);
        nextButton.interactable = (index != transform.childCount - 1);
        for (int i = 0; i < transform.childCount; i++) 
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }

    }
    public void ChangeGame(int change)
    {
        currentGame += change;
        SelectGame(currentGame);
    }
}
