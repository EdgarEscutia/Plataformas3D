using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField]float speed = 5f;


    [Header("Input Actions")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference run;


    CharacterController characterController;

    Camera mainCamera;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        run.action.Enable();


        move.action.performed += OnMove;
        move.action.performed += OnJump;
        run.action.performed += OnMove;
    }
    private void Update()
    {
        Vector3 movement = 
                            mainCamera.transform.right * rawMove.x + 
                            mainCamera.transform.forward * rawMove.z;

        float oldMovementMagnitude = movement.magnitude;

        Vector3 movementProjectedOnPlane = Vector3.ProjectOnPlane(movement, Vector3.up);

        movementProjectedOnPlane = movementProjectedOnPlane.normalized * oldMovementMagnitude;

        characterController.Move(movementProjectedOnPlane * speed * Time.deltaTime);
    }

    Vector3 rawMove = Vector3.zero;

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        rawMove = new Vector3(rawInput.x, 0, rawInput.y);
    }

    private void OnJump(InputAction.CallbackContext context)
    { 

    }


    
    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        run.action.Disable();

    }

}
