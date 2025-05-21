using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    [System.Serializable]
    public class ActionSet
    {
        public InputActionReference jump;
        public InputActionReference move;
        public InputActionReference crouch;
        public InputActionReference normalAttack;
        public InputActionReference strongAttack;
    }
    [Header("Input Actions Player")]
    public ActionSet actionSet;

}
