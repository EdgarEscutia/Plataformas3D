using NUnit.Framework;
using System;
using UnityEngine;

public class Class_Player : MonoBehaviour
{
    //public Class_Player Instance;
    public static Array Instances;
    public class Player
    {
        float playerHP;
        float enemyHP;

        float attack;
        float specialAttack;
        float ultimateCharge;
        float jump;

        bool playerAlive;
        bool enemyAlive;
    };
}
