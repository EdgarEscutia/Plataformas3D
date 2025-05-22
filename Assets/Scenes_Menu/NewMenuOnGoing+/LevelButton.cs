using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Vector3 zoomScale = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector3 originalScale;

    public string scene;

    private void Start()
    {
        originalScale = transform.localScale;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = zoomScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {

        if (!string.IsNullOrEmpty(scene))
        {
            SceneManager.LoadScene(scene);
        }
        
    }

}

