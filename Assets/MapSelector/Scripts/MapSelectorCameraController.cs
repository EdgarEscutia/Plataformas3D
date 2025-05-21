using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MapSelectorCameraController : MonoBehaviour
{
    [SerializeField] CinemachineCamera Yellowcamera;
    [SerializeField] CinemachineCamera Pinkcamera;
    [SerializeField] CinemachineCamera Greencamera;
    [SerializeField] CinemachineCamera StarsLandCamera;
    [SerializeField] CinemachineCamera Startcamera;

    [SerializeField] Button YellowBtn;
    [SerializeField] Button PinkBtn;
    [SerializeField] Button GreenBtn;
    [SerializeField] Button StarsBtn;
    [SerializeField] Button EarthPrueba_Btn;

    [SerializeField] Button ConfirmBtn;

    [SerializeField] string[] mapas;

    public int MapaElegido;


    private void Start()
    {
        Yellowcamera.enabled = false;
        Pinkcamera.enabled = false;
        Greencamera.enabled = false;
        StarsLandCamera.enabled = false;
        Startcamera.enabled = true;

        MapaElegido = 0;
    }
    void Update()
    {
        if(MapaElegido != 0)
        {
            ConfirmBtn.gameObject.SetActive(true);
        }
    }
    public void OnClickEarth()
    {

        Yellowcamera.enabled = false;
        Pinkcamera.enabled = false;
        Startcamera.enabled = false;
        StarsLandCamera.enabled = false;
        Greencamera.enabled = true;

        MapaElegido = 1;
    }

    public void OnClickStarsLand()
    {
        StarsLandCamera.enabled = true;
        Yellowcamera.enabled = false;
        Pinkcamera.enabled = false;
        Greencamera.enabled = false;
        Startcamera.enabled = false;

        MapaElegido = 2;
    }
    public void OnClickYellow()
    {

        Yellowcamera.enabled = true;
        Pinkcamera.enabled = false;
        Greencamera.enabled = false;
        Startcamera.enabled = false;
        StarsLandCamera.enabled = false;

        MapaElegido = 3;
    }

   
    public void OnClickPink()
    {

        Yellowcamera.enabled = false;
        Pinkcamera.enabled = true;
        Greencamera.enabled = false;
        Startcamera.enabled = false;
        StarsLandCamera.enabled = false;

        MapaElegido = 4;
    }

  



    public void SelectScene(string scene)
    { SceneManager.LoadScene(mapas[MapaElegido]);}



}
