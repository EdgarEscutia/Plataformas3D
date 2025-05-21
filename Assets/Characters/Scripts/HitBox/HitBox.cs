using System.Collections;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BaseFighter enemyFighter = other.gameObject.GetComponentInParent<BaseFighter>();

        if (gameObject.tag != enemyFighter.tag)
        {
            enemyFighter.NotifyHit();
           
            
           
        }
    }




}
