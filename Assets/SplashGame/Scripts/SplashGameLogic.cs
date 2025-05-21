using System.Collections;
using EasyTransition;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using DG.Tweening;

public class SplashGameLogic : MonoBehaviour
{
    [SerializeField] InputActionReference start;

    [SerializeField] string nextSceneName;
    [SerializeField] TransitionSettings transitionSettings;
    public float startDelay;

    private bool transitionIsFinish = false;

    [SerializeField] CanvasGroup[] images;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI pressKeyText;

    AudioSource audio;
    [SerializeField] AudioClip musicTheme;
    private void OnEnable()
    {
        start.action.Enable();

        start.action.performed += onStartGame;
    }
    private void Start()
    {
        audio = GetComponent<AudioSource>();

        StartCoroutine(FadeImagesSequentially());


    }

   

    public void onStartGame(InputAction.CallbackContext ctx)
    {
        if (transitionIsFinish)
        {
            TransitionManager.Instance().Transition(nextSceneName, transitionSettings, startDelay);
        }
    }

    private IEnumerator BlinkText()
    {
        title.text = "The Last Stars";

        while (true)
        {
            pressKeyText.text = " ";
            yield return new WaitForSeconds(0.5f);
            pressKeyText.text = "Press any key";
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator FadeImagesSequentially()
    {
        yield return new WaitForSeconds(startDelay);

        foreach (CanvasGroup image in images)
        {

            // Animación de rotación tipo flip
            Sequence flipSequence = DOTween.Sequence();

            flipSequence.Append(image.transform.DORotate(new Vector3(0, 0, 0), 0.6f).SetEase(Ease.OutBack));
            flipSequence.Join(DOTween.To(
                      () => image.alpha,
                      x => image.alpha = x,
                      1f,
                      0.6f
                  ));
            yield return flipSequence.WaitForCompletion();

            yield return new WaitForSeconds(0.3f);
        }
        audio.clip = musicTheme;
        audio.Play();
        transitionIsFinish = true;
        StartCoroutine(BlinkText());
    }

    private void OnDisable()
    {
        start.action.Disable();

        start.action.performed -= onStartGame;
    }
}