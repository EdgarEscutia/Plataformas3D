using UnityEngine;

public class Orientator : MonoBehaviour
{
    private float angularSpeed = 360f;

    public void SetAngularSpeed(float angularSpeed)
    {
        this.angularSpeed = angularSpeed;
    }

    public void OrientateTo(Vector3 desiredDirection)
    {
        float angleToApply = angularSpeed * Time.deltaTime;

        //Distancia angular
        float angularDistance = Vector3.SignedAngle(transform.forward, desiredDirection, Vector3.up);

        //Modulo (valor en positivo)
        float realAngleToApply =
            Mathf.Sign(angularDistance) *  //O vale 1f o -1f
            Mathf.Min(angleToApply, Mathf.Abs(angularDistance)); //o e lo que tocaba girar o un poco menos pq ya ha llegado

        Quaternion rotationToApply = Quaternion.AngleAxis(realAngleToApply, Vector3.up);


        transform.rotation = rotationToApply * transform.rotation; //La rotacion sera aplicada a la rotacion del player
    }
}
