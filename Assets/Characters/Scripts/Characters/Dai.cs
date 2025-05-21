using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.PlayerSettings.SplashScreen;

public class Dai : BaseFighter
{
    public static Dai instance;

    private void Awake()
    {
        instance = this;
    }

}