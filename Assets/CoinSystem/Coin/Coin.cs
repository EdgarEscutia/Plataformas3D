using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    static public int count;
    static public int maxCount;


    void Awake()
    {

        maxCount++;

    }

    void OnTriggerEnter()
    {
        count--;
        Destroy(gameObject);
        Debug.Log(count);
    }

    void Start()
    {
        count = maxCount;
    }
}
