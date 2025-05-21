using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowEnemy : MonoBehaviour
{
    public float speed;
    public float minDistance;

    public float touchDistance;

    public Transform player1;

    private bool doingCombo;

    private void Update()
    {
        if(!player1)
        {
            player1 = GameObject.FindGameObjectWithTag("Player1").transform;
        }
        //AI DISTANCE TO PLAYER
        if (Vector2.Distance(transform.position, player1.position) > minDistance)
        {
            transform.position = 
                Vector2.MoveTowards(transform.position, player1.position, speed * Time.deltaTime);
        }
        else if(!doingCombo) //ATACK CODE
        {
            Debug.Log("COMBOS");
            doingCombo = true;
        }

        transform.LookAt(player1);
    }

    private void Start()
    {

    }
}
