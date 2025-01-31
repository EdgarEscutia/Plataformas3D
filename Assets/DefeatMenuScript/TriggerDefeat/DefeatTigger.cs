using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] Canvas defeatCanvas;


    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0f;
        defeatCanvas.gameObject.SetActive(true);
    }
}
