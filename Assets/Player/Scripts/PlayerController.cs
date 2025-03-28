using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{

    public static PlayerController instance;

    [Header("Move Settings")]
    [SerializeField]float speedWalk = 5f;
    [SerializeField]float speedRun = 10f;
    [SerializeField] float verticalSpeedOnGrounded = -5f;
    [SerializeField] float jumpVelocity = 10f;


    public enum OrientationMode
    {
        ToMovementDirection,
        ToCameraForward,
        ToTarget,
    };
    [Header("Orientation")]
    [SerializeField] OrientationMode orientationMode = OrientationMode.ToMovementDirection;
    [SerializeField] Transform orientationTarget;
    [SerializeField] float angularSpeed = 720f;


    [Header("Input Actions")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference run;


    CharacterController characterController;

    Ragdollizer ragdollizer;

    Camera mainCamera;

    Orientator orientator;
    
    float speed;

    protected override void Awake()
    {
        base.Awake();


        instance = this;

        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        orientator = GetComponent<Orientator>();
        orientator.SetAngularSpeed(angularSpeed);




        Time.timeScale = 1f;
        //para arreglar las animaciones.
        //animator.keepAnimatorControllerStateOnDisable = true;

        speed = speedWalk;
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

        run.action.started += OnRun;
        run.action.canceled += OnRun;

        run.action.performed += OnMove;

    }
    protected  void Update()
    {
        UpdateMovementOnPlay();
        UpdateVerticalMovement();
        UpdateOrientation();
        UpdateAnimation();
    }

    Vector3 lastNormalizedVelocity = Vector3.zero;

    private void UpdateMovementOnPlay()
    {
        Vector3 movement =
                        mainCamera.transform.right * rawMove.x +
                        mainCamera.transform.forward * rawMove.z;

        float oldMovementMagnitude = movement.magnitude;

        Vector3 movementProjectedOnPlane = Vector3.ProjectOnPlane(movement, Vector3.up);

        movementProjectedOnPlane = movementProjectedOnPlane.normalized * oldMovementMagnitude;

        characterController.Move(movementProjectedOnPlane * speed * Time.deltaTime);

        lastNormalizedVelocity = movementProjectedOnPlane;
    }

    float gravity = -9.8f;
    float verticalVelocity;

    void UpdateVerticalMovement()
    {
        verticalVelocity += gravity * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
        //lastVelocity = move

        lastNormalizedVelocity.y = verticalVelocity;

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

    void UpdateOrientation()
    {
        Vector3 desiredDirection = Vector3.forward;
        switch(orientationMode)
        { 
            case OrientationMode.ToMovementDirection:
                desiredDirection = lastNormalizedVelocity;
                break;
            case OrientationMode.ToCameraForward:
                desiredDirection = mainCamera.transform.forward;
                break;
            case OrientationMode.ToTarget:
                desiredDirection = orientationTarget.position - transform.position; 
                break;
        }
        desiredDirection.y = 0f;

        orientator.OrientateTo(desiredDirection);
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

        run.action.started -= OnRun;
        run.action.canceled -= OnRun;

        move.action.performed -= OnJump;

    }

    void OnRun(InputAction.CallbackContext context)
    {
        speed = run.action.IsPressed() ? speedRun :speedWalk;

    }

    //private void OnHitRecieved(IHitter hitter,HitCollider hitCollider, HurtCollider hurtCollider)
    //{
    //    ragdollizer.Ragdollize();
    //    Invoke(nameof(Desactivate), 2f);
    //    Invoke(nameof(Resurrect), 5f);

    //}

    //private void Resurrect()
    //{
    //    gameObject.SetActive(true);
    //    ragdollizer.DeRagdollize();
    //}

    //private void Desactivate()
    //{
    //    gameObject.SetActive(false);
    //}

    override protected float GetCurrentVerticalSpeed()
    {
        return verticalVelocity;
    }

    override protected float GetJumpSpeed()
    {
        return jumpVelocity;
    }

    override protected bool IsRunning()
    {
        return speed == speedRun;
    }

    override protected bool IsGrounded()
    {
        return characterController.isGrounded;
    }

    override protected Vector3 GetLastNormalizedVelocity()
    {
        return lastNormalizedVelocity;
    }
}
