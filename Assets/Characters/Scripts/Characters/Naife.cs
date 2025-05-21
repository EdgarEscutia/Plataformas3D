using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.PlayerSettings.SplashScreen;

public class Naife : BaseFighter
{
    public static Naife instance;

    private void Awake()
    {
        instance = this;
    }

}