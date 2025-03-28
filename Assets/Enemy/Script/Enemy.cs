using System;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{


    [SerializeField] float detectionDistance = 15f;
    [SerializeField] float shootingDistance = 10f;


    NavMeshAgent agent;
    WeaponManager weaponManager;

    BaseState[] allStates;

    [Header("States")]
    [SerializeField] BaseState chasingState;
    [SerializeField] BaseState notChasingState;
    [SerializeField] BaseState shootingState;

    Orientator orientator;

    BaseState currentState;

    protected override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();

        allStates = GetComponents<BaseState>();
        weaponManager = GetComponentInChildren<WeaponManager>();

        foreach (BaseState s in allStates)
            { s.Init(this);}


        orientator = GetComponent<Orientator>();



    }

    private void Start()
    {
        ChangeState(notChasingState);
    }

    public Transform target;
    private void Update()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;

        //EJECUTAR SENTIDOS
        target = CheckSenses();

        //TOMA DE DECISIONES
        if (target == null) {
            ChangeState(notChasingState);
        }
        else if(TargetIsInRange())
        {
            ChangeState(shootingState);
        }
        else {
            ChangeState(chasingState);
        }

        UpdateAnimation();

    }

    void ChangeState(BaseState newState)
    {
        if(currentState != newState)
        {
            if(currentState != null)
            {currentState.enabled = false;}

            currentState = newState;
            if (currentState != null) { currentState.enabled = true; }
        }
    }

    private Transform CheckSenses()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;
        return (
            PlayerController.instance.gameObject.activeSelf &&
            (Vector3.Distance(playerPosition, transform.position) < detectionDistance)
            ) ? PlayerController.instance.transform : null;
       
    }

    bool TargetIsInRange()
    {
        return (target != null) && Vector3.Distance(target.position, transform.position) < shootingDistance;
       
    }


    #region Enity Implementation
    protected override float GetCurrentVerticalSpeed()
    {
        return 0f;
    }

    protected override float GetJumpSpeed()
    {
        return 0f;
    }

    protected override bool IsRunning()
    {
        return true;
    }

    protected override bool IsGrounded()
    {
        return true;
    }

    protected override Vector3 GetLastNormalizedVelocity()
    {
        return agent.velocity.normalized;
    }
    #endregion

    #region AI
    internal NavMeshAgent GetAgent()
    { return agent; }

    internal Transform GetTarget()
    {
        return target;
    }

    internal WeaponManager GetWeaponManager() { return weaponManager; }

    internal Orientator GetOrientator()
    {
        return orientator;
    }

    #endregion
}
