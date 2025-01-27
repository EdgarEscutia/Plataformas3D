using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Canvas : MonoBehaviour
{
    public TMP_Text score;
    [SerializeField] Canvas victoryCanvas;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TMP_Text>();
        victoryCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        score.text = $"{Coin.count} / {Coin.maxCount}";
        
        if(Coin.count == 0)
        {
            victoryCanvas.gameObject.SetActive(true);
        }
    }
}
