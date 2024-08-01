using UnityEngine;
using UnityEngine.AI;

public class CharacterMovementController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f; 
    public float gravity = 9.81f; 
    public Animator animator; 
    public Transform destination; 

    private CharacterController characterController;
    private NavMeshAgent navMeshAgent;
    private float verticalVelocity = 0f; 
    public bool isAutoMoving = false; 

    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    [Range(0.0f, 3f)]
    public float RotationSmoothTime = 0.12f;

    private Transform _mainCameraTransform;

    private void Awake()
    {
        if (_mainCameraTransform == null)
        {
            _mainCameraTransform = Camera.main.transform;
        }
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            navMeshAgent.speed = moveSpeed;
        }

        navMeshAgent.enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isAutoMoving)
            {
                StopAutoMove();
            }
        }

        if (isAutoMoving)
        {
            // Update the NavMeshAgent destination
            if (navMeshAgent != null && destination != null)
            {
                if (navMeshAgent.enabled == false)
                    navMeshAgent.enabled = true;

                navMeshAgent.SetDestination(destination.position);


                if (navMeshAgent != null && !navMeshAgent.pathPending)
                {
                    if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                    {
                        if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                        {
                            StopAutoMove(); // Stop auto-move when destination is reached
                        }
                    }
                }
            }

            if (animator != null)
            {
                bool isMoving = navMeshAgent.velocity.magnitude > 0.05;
                animator.SetBool("isMovingForward", isMoving);
            }



        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (characterController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        if (inputDirection != Vector3.zero)
        {
            //Here calculating rotation as per horizontal input x
            _targetRotation = Mathf.Atan2(inputDirection.x, 0) * Mathf.Rad2Deg + _mainCameraTransform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

            //Here calculating if the character will move forward or back using vertical input z
            _targetRotation = Mathf.Atan2(0, inputDirection.z) * Mathf.Rad2Deg + _mainCameraTransform.eulerAngles.y;

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;


            if (moveVertical != 0)
            {
                //Here only moving the character if we get vertical input
                characterController.Move(targetDirection.normalized * (moveSpeed * Time.deltaTime) +
                                 new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
            }


            HandleAnimation(moveVertical);
        }
    }

    void HandleAnimation(float moveVertical) { 

        //Handling animation for forward or backward
        if (animator != null)
        {

            if (moveVertical < 0)
            {
                animator.SetBool("isMovingForward", false);
                animator.SetBool("isMovingBackward", true);
            }
            else if (moveVertical > 0)
            {
                animator.SetBool("isMovingBackward", false);
                animator.SetBool("isMovingForward", true);
            }
            else
            {
                animator.SetBool("isMovingForward", false);
                animator.SetBool("isMovingBackward", false);
            }
        }
    }

    public void StartAutoMove(Transform targetDestination)

    {
        if (navMeshAgent != null)
        {
            isAutoMoving = true;
            destination = targetDestination;
        }
    }

    private void StopAutoMove()
    {
        if (navMeshAgent != null)
        {
            isAutoMoving = false;
            navMeshAgent.SetDestination(transform.position);
            navMeshAgent.enabled = false;
            destination = null;
        }

    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
    }
}
