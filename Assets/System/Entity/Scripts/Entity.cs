using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] float animationSmoothingSpeed = 10f;

    Animator animator;


    virtual protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.keepAnimatorStateOnDisable = true;
    }



    Vector3 currentAnimationSpeed = Vector3.zero;
    Vector3 desiredAnimationSpeed = Vector3.zero;

    virtual protected void UpdateAnimation()
    {              
        float speedMultiplayer = IsRunning() ? 2f : 1f;

        //Vector3 localVelocity = transform.InverseTransformDirection(lastVelocity);
        //Vector3 normalizedLocalVelocity = localVelocity / speed;

        desiredAnimationSpeed = speedMultiplayer * transform.InverseTransformDirection(GetLastNormalizedVelocity());

        //CurrentAniamtionSpeed tiene que tender hacia desired animation speed

        Vector3 direction = desiredAnimationSpeed - currentAnimationSpeed;
        float distance = direction.magnitude;
        float smoothingStep = (animationSmoothingSpeed * Time.deltaTime);
        float distanceToApply = Mathf.Min(distance, smoothingStep);

        currentAnimationSpeed += direction.normalized * distanceToApply;



        animator.SetFloat("SidewardVelocity", currentAnimationSpeed.x);
        animator.SetFloat("ForwardVelocity", currentAnimationSpeed.z);
        animator.SetBool("IsGrounded", IsGrounded());
        float clampedVerticalVelocity =
            Mathf.Clamp(GetCurrentVerticalSpeed(), -GetJumpSpeed(), GetJumpSpeed());

        float verticalProgress = Mathf.InverseLerp(
            GetJumpSpeed(), -GetJumpSpeed(), clampedVerticalVelocity
            );

        animator.SetFloat("VerticalProgress", IsGrounded() ? 1f : verticalProgress);
    }

    abstract protected float GetCurrentVerticalSpeed();

    abstract protected float GetJumpSpeed();

    abstract protected bool IsRunning();

    abstract protected bool IsGrounded();

    abstract protected Vector3 GetLastNormalizedVelocity();
}
