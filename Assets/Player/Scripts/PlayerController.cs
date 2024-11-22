using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField]float speed = 5f;
    [SerializeField] float verticalSpeedOnGrounded = -5f;
    [SerializeField] float jumpVelocity = 10f;


    [Header("Input Actions")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference run;


    CharacterController characterController;

    Camera mainCamera;
    Animator animator;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        run.action.Enable();


        move.action.performed += OnMove;
        move.action.started += OnMove;
        move.action.canceled += OnMove;

        jump.action.performed += OnJump;

        run.action.performed += OnMove;

    }
    private void Update()
    {
        UpdateMovementOnPlay();
        UpdateVerticalMovement();
        UpdateAnimation();
    }

    Vector3 lastVelocity = Vector3.zero;

    private void UpdateMovementOnPlay()
    {
        Vector3 movement =
                        mainCamera.transform.right * rawMove.x +
                        mainCamera.transform.forward * rawMove.z;

        float oldMovementMagnitude = movement.magnitude;

        Vector3 movementProjectedOnPlane = Vector3.ProjectOnPlane(movement, Vector3.up);

        movementProjectedOnPlane = movementProjectedOnPlane.normalized * oldMovementMagnitude;

        characterController.Move(movementProjectedOnPlane * speed * Time.deltaTime);

        lastVelocity = movementProjectedOnPlane;
    }

    float gravity = -9.8f;
    float verticalVelocity;

    void UpdateVerticalMovement()
    {
        verticalVelocity += gravity * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);

        lastVelocity.y = verticalVelocity;

        if (characterController.isGrounded)
        {
            verticalVelocity = verticalSpeedOnGrounded;
        }

        if(mustJump)
        {
            mustJump = false;

            if(characterController.isGrounded)
            {
                verticalVelocity = jumpVelocity;
            }
        }
    }

    void UpdateAnimation()
    {
        animator.SetFloat("SidewardVelocity", lastVelocity.x);
        animator.SetFloat("ForwardVelocity", lastVelocity.z);
    }


    Vector3 rawMove = Vector3.zero;

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        rawMove = new Vector3(rawInput.x, 0, rawInput.y);
    }


    bool mustJump;
    private void OnJump(InputAction.CallbackContext context)
    { 
        mustJump = true;
    }


    
    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        run.action.Disable();

        move.action.performed -= OnMove;
        move.action.started-= OnMove;
        move.action.canceled -= OnMove;


        move.action.performed -= OnJump;

    }

}
