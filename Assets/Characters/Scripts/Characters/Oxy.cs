using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.PlayerSettings.SplashScreen;

public class Oxy : BaseFighter
{
    public static Oxy instance;

    private void Awake()
    {
        instance = this;
    }

}