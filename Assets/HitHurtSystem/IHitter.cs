using UnityEngine;

public interface IHitter
{
    public float GetDamage(); // Devolver el daño que ahce el Hitter
    public Transform GetTransform(); //Devolver la transform del agresor
}
