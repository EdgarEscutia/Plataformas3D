using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VictoryManager : MonoBehaviour
{
    private Animator transitionAnimator;
    [SerializeField] float transitionTime = 5f * 1000f;
    [SerializeField] Canvas canvas;

    void Update()
    {
        bool playerMuerto = false;

       foreach (BaseFighter bf in BaseFighter.fighterList)
       {
           playerMuerto |= bf.currentHealth < 0f;

           if(bf.currentHealth < 0f)
           {
               playerMuerto = true;
               StartCoroutine(SceneLoad());

           }
       }
       if (playerMuerto)
       {
           StartCoroutine(SceneLoad());
       }
    }

    public IEnumerator SceneLoad()
    {
        //if (baseFighter.currentHealth <= 0)
        //{
        canvas.gameObject.SetActive(true);
        transitionAnimator.SetTrigger("StartTransition");

        

        yield return new WaitForSeconds(transitionTime);

        ChargeScene();
        //}

    }

    public static void ChargeScene()
    {
        Debug.Log("Siguiente escena");
        SceneManager.LoadScene("Options");
    }
}
