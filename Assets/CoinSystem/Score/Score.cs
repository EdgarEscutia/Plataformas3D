using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Canvas : MonoBehaviour
{
    public TMP_Text score;
    [SerializeField] Canvas victoryCanvas;

    [SerializeField] GameObject Puerta1;
    [SerializeField] GameObject Puerta2;
    [SerializeField] GameObject Puerta3;
    [SerializeField] GameObject Puerta4;
    [SerializeField] GameObject Puerta5;
    [SerializeField] GameObject Puerta6;


    [SerializeField] int puerta1;
    [SerializeField] int puerta2;
    [SerializeField] int puerta3;
    [SerializeField] int puerta4;
    [SerializeField] int puerta5;
    [SerializeField] int puerta6;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TMP_Text>();
        victoryCanvas.gameObject.SetActive(false);
        Puerta1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        score.text = $"{Coin.count} / {Coin.maxCount}";
        
        if(Coin.count == puerta1)
        { Puerta1.SetActive(false);}
           
        
        if (Coin.count == puerta2)
        { Puerta2.SetActive(false); }
           
       
        if (Coin.count == puerta3)
        {Puerta3.SetActive(false); }
            
       
        if (Coin.count == puerta4)
        { Puerta4.SetActive(false);}

        if (Coin.count == puerta5)
        { Puerta5.SetActive(false); }

        if (Coin.count == puerta6)
        { Puerta6.SetActive(false); }


        if (Coin.count == 0)
        {victoryCanvas.gameObject.SetActive(true);}
            
        
    }
}
