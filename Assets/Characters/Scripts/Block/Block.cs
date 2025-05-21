using UnityEngine;

public class Block : MonoBehaviour
{
    private BaseFighter fighter;


    private void OnTriggerEnter(Collider other)
    {
        fighter = gameObject.GetComponentInParent<BaseFighter>();

        if(other.gameObject.CompareTag("HitBox"))
        {
            fighter.NotifyDamageReceived(1f);

        }



    }

}
