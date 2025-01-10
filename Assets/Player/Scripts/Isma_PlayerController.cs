using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Isma_PlayerController : MonoBehaviour
{
    //public static PlayerController instance;

    [Header("Movemet Settings")]
    [SerializeField] float speed = 5f;
    [SerializeField] float verticalSpeedOnGrounded = -5f;
    [SerializeField] float jumpVelocity = 5f;

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
    Camera mainCamera;
    Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Camera mainCamera;
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

        //hurtCollider.onHitReceived.AddListener();
    }

    private void Update()
    {
        UpdateMovementOnPlane();
        UpdateVerticalMovement();
        UpdateOrientation();
        UpdateAnimation();

    }

    Vector3 lastVelocity = Vector3.zero;

    private void UpdateMovementOnPlane()
    {
        Vector3 movement =
            mainCamera.transform.right * rawMove.x +
            mainCamera.transform.forward * rawMove.z;
        float oldMovementMagnitude = movement.magnitude;

        Vector3 movementProjectedOnPlane =
            Vector3.ProjectOnPlane(movement, Vector3.up);

        movementProjectedOnPlane = movementProjectedOnPlane.normalized * oldMovementMagnitude;


        characterController.Move(movementProjectedOnPlane * speed * Time.deltaTime);
        lastVelocity = movementProjectedOnPlane * speed;
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
        if (mustJump)
        {
            mustJump = false;
            if (characterController.isGrounded)
            {
                verticalVelocity = jumpVelocity;
            }
        }
    }

    void UpdateOrientation()
    {
        Vector3 desiredDirection = Vector3.forward;
        switch (orientationMode)
        {
            case OrientationMode.ToMovementDirection:
                desiredDirection = lastVelocity;
                break;
            case OrientationMode.ToCameraForward:
                desiredDirection = mainCamera.transform.forward;
                break;
            case OrientationMode.ToTarget:
                desiredDirection = orientationTarget.position - transform.position;
                break;
        }
        desiredDirection.y = 0f;

        float angleToApply = angularSpeed * Time.deltaTime;
        // Distancia angular entre transform.forward y desiredDirection
        float angularDistance = Vector3.SignedAngle(transform.forward, desiredDirection, Vector3.up);
        float realAngleToApply =
            Math.Abs(angularDistance) *                             // 0 vale 1f, o vale -1f
            Math.Min(angleToApply, Mathf.Abs(angularDistance));     // 0 es lo que tocaba girar, o
                                                                    // es un poco menos porque ya he llegado
        Quaternion rotationToApply = Quaternion.AngleAxis(realAngleToApply, Vector3.up);
        transform.rotation = rotationToApply * transform.rotation;
    }


    void UpdateAnimation()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(lastVelocity);
        Vector3 normalizedField = localVelocity / speed;

        animator.SetFloat("SidewardVelocity", lastVelocity.x);
        animator.SetFloat("ForwardVelocity", lastVelocity.z);
    }

    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        run.action.Disable();

        move.action.performed -= OnMove;
        move.action.started -= OnMove;
        move.action.canceled -= OnMove;

        jump.action.performed -= OnJump;

        //hurtCollider.onHitReceived.RemoveListener(OnHitReceived);
    }


    //private void OnHitReceived(HitCollider hitCollide, HurtCollider hurtCollider)
    //{
    //    gameObject.SetActive(false);
    //    Invoke(nameof(Resurrect), 3f);
    //}

    void Resurrect()
    {
        gameObject.SetActive(true);
    }

    Vector3 rawMove = Vector3.zero;

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        rawMove = new Vector3(rawInput.x, 0f, rawInput.y);
    }

    bool mustJump;
    private void OnJump(InputAction.CallbackContext context)
    {
        mustJump = true;
    }
}

