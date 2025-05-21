using System.Collections;
using EasyTransition;
using UnityEngine;
using UnityEngine.Video;

public class CompanyGameLogic : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] string nextSceneName;
    [SerializeField] TransitionSettings transitionSettings;
    public float startDelay;

    private bool isTransitioning = false;


    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer videoPlayer)
    {
        if (isTransitioning) 
        return;
    }

    private void Update()
    {
        if (isTransitioning)
        {
            return;
        }

        isTransitioning = true;
        TransitionManager.Instance().Transition(nextSceneName, transitionSettings, startDelay);


    }


}
