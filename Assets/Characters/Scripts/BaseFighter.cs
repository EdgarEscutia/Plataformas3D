using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class BaseFighter : MonoBehaviour, IDamageable
{
    public static List<BaseFighter> fighterList = new();
    VictoryManager victoryManager;


    [Header("Movement Settings")]

    public bool characterCanJump = true;

    public float speed;

    public float jumpForce = 5f;
    private bool isGrounded = true;

    Rigidbody characterRb;

    [Header("Animation Settings")]

    Animator anim;

    GameObject enemyPlayer;
    private SpriteRenderer spriteRenderer;

    [Header("Attacks Parameters")]

    private bool canAttack;
    private int nroAttack;
    private bool canAirAttack = true;
    public bool isBlocking = false;
    bool inputsHaveBeenInited = false;
    private bool hitted;
    [SerializeField] public int hitCount;
    float knockbackForce = 250f;
    bool isInvulnerable = false;

    public float health;
    public float currentHealth;


    

    PlayerMovements movements;

    private Coroutine resetHitCoroutine;



    public void InitInputs(PlayerMovements.ActionSet actionSetPl)
    {
        movements = GetComponent<PlayerMovements>();
        movements.actionSet = actionSetPl;

        inputsHaveBeenInited = true;

        EnableInputs();
    }

    private void OnEnable()
    {
        fighterList.Add(this);
        if (inputsHaveBeenInited)
            { EnableInputs(); }

    }
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitCount = 0;

        characterRb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = health;

        hitted = false;

        canAttack = true;
        nroAttack = 0;


        if (gameObject.tag == "Player1")
        {
            enemyPlayer = GameObject.FindGameObjectWithTag("Player2");
        }
        else
        {
            enemyPlayer = GameObject.FindGameObjectWithTag("Player1");
        }
    }

    private void Update()
    {
        UpdateAnimatorParameters();

        Vector3 direction = enemyPlayer.transform.position - transform.position;

        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && nroAttack == 0)
        {
            canAttack = true;
        }
      

        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }

    }

    private void UpdateAnimatorParameters()
    {

        float horizontalSpeed = characterRb.linearVelocity.x;

        if(gameObject.tag == "Player1")
        {
            anim.SetFloat("Speed", horizontalSpeed);
        }
        else
        {
            anim.SetFloat("Speed", -horizontalSpeed);

        }

        if (horizontalSpeed > 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (horizontalSpeed < -0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
       }
    }

    void FixedUpdate()
    {
        if(!isBlocking)
        {
            UpdateMovementOnPlane();
        }
    }

    Vector3 rawMove = Vector3.zero;


    void UpdateMovementOnPlane()
    {

        if (!hitted && !isInvulnerable)
        {
            Vector3 moveDirection = rawMove * speed;
            Vector3 velocity = new Vector3(moveDirection.x, characterRb.linearVelocity.y, 0);

            characterRb.linearVelocity = velocity;
        }

    }


    void OnJump(InputAction.CallbackContext ctx)
    {
        if (characterCanJump)
        {
            if (ctx.performed &&
                isGrounded &&
                !isInvulnerable)
            {
                isGrounded = false;
                characterRb.linearVelocity = new Vector3(characterRb.linearVelocity.x, jumpForce, characterRb.linearVelocity.z);
                anim.SetBool("IsGrounded", false);
                anim.SetTrigger("Jump");
            }
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        rawMove = new Vector3(rawInput.x, 0f, rawInput.y);
    }

    private void OnCrouch(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (isGrounded)
            {
                isBlocking = true;

                anim.SetBool("crouch", true);
                canAttackTrue();
                nroAttack = 0;
                anim.SetInteger("AttackCount", nroAttack);
            }
        }
        else if (ctx.canceled)
        {
            anim.SetBool("crouch", false);

            ResetBlocking();

        }
    }

    protected virtual void OnNormalAttack(InputAction.CallbackContext ctx)
    {
        float attackForce = 5f;

        if (ctx.performed && isGrounded)
        {
            Vector3 attackImpulse = Vector3.zero;

            if (rawMove.z < -0.1f)
            {
                anim.SetTrigger("normalAttackDown");

            }
            else if (rawMove.z > 0.1f)
            {
                anim.SetTrigger("normalAttackUp");

            }
            else if (rawMove.x > 0.1f)
            {
                anim.SetTrigger("normalAttackRight");

            }
            else if (canAttack && nroAttack < 3)
            {
                if (spriteRenderer.flipX == false)
                {
                    Vector3 forceDirection = transform.right * attackForce; // Empuje hacia adelante

                    nroAttack++;
                    if (nroAttack == 1)
                        anim.SetInteger("AttackCount", nroAttack);

                    canAttack = false;


                    characterRb.AddForce(forceDirection, ForceMode.Impulse);
                }
                else
                {
                    Vector3 forceDirection = -transform.right * attackForce; // Empuje hacia adelante

                    nroAttack++;
                    if (nroAttack == 1)
                        anim.SetInteger("AttackCount", nroAttack);

                    canAttack = false;


                    characterRb.AddForce(forceDirection, ForceMode.Impulse);
                }
               

            }

            characterRb.linearVelocity += new Vector3(attackImpulse.x, 0, 0);

        }
        else if (ctx.performed && !isGrounded && canAirAttack)
        {
            anim.SetTrigger("normalAttackAir");
            canAirAttack = false;
            StartCoroutine(AirAttackCooldown(0.5f));

        }
    }

    protected virtual void OnStrongAttack(InputAction.CallbackContext ctx)
    {

        if (ctx.performed && isGrounded)
        {
            if (rawMove.z < 0f)
            {
                //anim.SetBool("strongAttackDownCharge", true);

            }
            else
            {
                anim.SetBool("strongAttackCharge", true);

            }
        }
        if (ctx.canceled)
        {
            if (rawMove.z < 0f)
            {
                //anim.SetBool("strongAttackDownCharge", false);

                //anim.SetBool("strongAttackCharge", false);


                //anim.SetTrigger("strongAttackDown");

            }
            else
            {
                //anim.SetBool("strongAttackDownCharge", false);

                anim.SetBool("strongAttackCharge", false);


                anim.SetTrigger("strongAttack");
            }


        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("IsGrounded", true);
        }
    }



    private void OnDisable()
    {
        fighterList.Remove(this);

        DisableInput();

    }



    public void VerificaCombo()
    {


        if (canAttack)
        {
            nroAttack = 0;
            canAttack = false;
            anim.SetInteger("AttackCount", nroAttack);
        }
        else
        {
            if (nroAttack > 1)
                anim.SetInteger("AttackCount", nroAttack);
        }
    }

    public void canAttackTrue()
    {
        canAttack = true;
    }

    IEnumerator AirAttackCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        canAirAttack = true;
    }

   
    public bool HasTakenDamage { get { return hitted;  } set { hitted = value;  } }
    public void NotifyDamageReceived(float damageAmount)
    {
        // Ataco

        if (!hitted)
        {
            if(isBlocking)
            {
                currentHealth -= 0.5f;
            }
            else
            {
                currentHealth -= damageAmount;
                anim.SetTrigger("hit");
                hitted = true;
            }

            Invoke(nameof(ResetHit), 0.3f);
        }
    }
    public void ResetHit()
    {
        hitted = false;
    }


    public void ResetBlocking()
    {
        isBlocking = false;
    }


    #region InputManagement
    private void EnableInputs()
    {
        movements.actionSet.move.action.Enable();

        movements.actionSet.jump.action.Enable();

        movements.actionSet.normalAttack.action.Enable();

        movements.actionSet.strongAttack.action.Enable();


        movements.actionSet.crouch.action.Enable();

        movements.actionSet.jump.action.performed += OnJump;
        movements.actionSet.jump.action.canceled += OnJump;

        movements.actionSet.crouch.action.performed += OnCrouch;
        movements.actionSet.crouch.action.canceled += OnCrouch;

        movements.actionSet.normalAttack.action.performed += OnNormalAttack;
        movements.actionSet.normalAttack.action.canceled += OnNormalAttack;

        movements.actionSet.strongAttack.action.performed += OnStrongAttack;
        movements.actionSet.strongAttack.action.canceled += OnStrongAttack;

        movements.actionSet.move.action.performed += OnMove;
        movements.actionSet.move.action.started += OnMove;
        movements.actionSet.move.action.canceled += OnMove;
    }

    private void DisableInput()
    {
        movements.actionSet.jump.action.Disable();

        movements.actionSet.jump.action.performed -= OnJump;
        movements.actionSet.jump.action.canceled -= OnJump;

        movements.actionSet.normalAttack.action.Disable();

        movements.actionSet.normalAttack.action.performed -= OnNormalAttack;
        movements.actionSet.normalAttack.action.canceled -= OnNormalAttack;

        movements.actionSet.crouch.action.performed -= OnCrouch;
        movements.actionSet.crouch.action.canceled -= OnCrouch;

        movements.actionSet.move.action.Disable();

        movements.actionSet.move.action.performed -= OnMove;
        movements.actionSet.move.action.started -= OnMove;
        movements.actionSet.move.action.canceled -= OnMove;
    }

    #endregion

    internal void NotifyHit()
    {
        hitCount += 1;
        anim.SetInteger("hitCount", hitCount);
        NotifyDamageReceived(5f);

        if (resetHitCoroutine != null)
        {
            StopCoroutine(resetHitCoroutine);
        }
        resetHitCoroutine = StartCoroutine(ResetHitCount());

        if (hitCount >= 3 && !isBlocking)
        {
            Vector3 flatDirection = characterRb.position - enemyPlayer.transform.position;
            flatDirection.y = 0f;
            flatDirection.Normalize();
            flatDirection.y = 0.8f; // Salto hacia atrás
            flatDirection.z = 0f;

            characterRb.AddForce(flatDirection * knockbackForce * Time.deltaTime, ForceMode.Impulse);

            hitted = true;

            StartCoroutine(IsInvunerable());
            Invoke(nameof(ResetHit), 0.5f);
        }

        
    }


    public IEnumerator ResetHitCount()
    {
        yield return new WaitForSeconds(1f);
        hitCount = 0;
        anim.SetInteger("hitCount", hitCount);
    }

    public IEnumerator IsInvunerable()
    {
        isInvulnerable = true;
        anim.SetBool("IsInvunerable", isInvulnerable);
        yield return new WaitForSeconds(2f);
        isInvulnerable = false;
        anim.SetBool("IsInvunerable", isInvulnerable);

    }


}
