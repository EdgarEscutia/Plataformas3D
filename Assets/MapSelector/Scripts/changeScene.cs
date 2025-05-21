using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    [Header("Cambio Escena")]
    public TransitionSettings transition;
    public float loadDelay;
    public void ChangeScene(string name)
    {
        //SceneManager.LoadScene(name);
        TransitionManager.Instance().Transition(name, transition, loadDelay);

    }
}
