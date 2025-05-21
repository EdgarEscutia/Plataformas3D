using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.PlayerSettings.SplashScreen;

public class Tempus : BaseFighter
{
    public static Tempus instance;

    private void Awake()
    {
        instance = this;
    }

}