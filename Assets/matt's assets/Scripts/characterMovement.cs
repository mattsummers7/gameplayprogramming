using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterMovement : MonoBehaviour
{

    Animator animator;

    int isRunningNHash;
    int isStrafeNHash;
    int isStrafeEHash;
    int isStrafeSHash;
    int isStrafeWHash;
    int isLockedOn;
    int hasAttackedHash;
    int isJumpingHash;
    int isFallingHash;
    int hasInteractedHash;

    PlayerInput input;

    Vector2 currentMovement;
    Vector2 lookMovement;
    Vector3 moveDir;
    private Vector3 playerMovement;
    bool movementPressed;
    bool attackPressed;
    bool jumpPressed = false;
    bool lookPressed;
    bool lockedOn;
    public bool interactActive;
    public bool cutsceneActive;

    float gravity = -9.81f;
    float gravityMultiplier = 3.0f;
    float velocity;
    public float jumpValue;

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] Transform cam;

    private bool isGrounded = false;
    public float speed = 10;
    int jumpsRemaining;
    //max jumps 0 = 1 jump
    public int maxJumps = 0;
    public bool defaultMovement;
    
    float speedMultiplier;
    private Rigidbody rb;
    public ParticleSystem powerUpParticles;

    void Awake()
    {
        input = new PlayerInput();
        jumpsRemaining = maxJumps;

        input.CharacterControls.Movement.performed += ctx =>
        {
            Debug.Log("Current movement vector " + ctx);
            currentMovement = ctx.ReadValue<Vector2>();
            playerMovement.x = currentMovement.x * speedMultiplier;
            playerMovement.z = currentMovement.y * speedMultiplier;

            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };

        input.CharacterControls.Movement.canceled += ctx =>
        {
            Debug.Log("Movement cancelled");
            currentMovement = Vector2.zero;
        };

        input.CharacterControls.Attack.performed += ctx =>
        {
            attackPressed = ctx.ReadValueAsButton();
        };

        

        input.CharacterControls.Jump.started += ctx =>
        {
            jumpPressed = ctx.ReadValueAsButton();
        };

        input.CharacterControls.Jump.canceled += ctx =>
        {
            jumpPressed = ctx.ReadValueAsButton();
        };

        input.CharacterControls.Jump.started += onJump;
        

        input.CharacterControls.Look.performed += ctx =>
        {
            lookMovement = ctx.ReadValue<Vector2>();

            lookPressed = lookMovement.x != 0 || lookMovement.y != 0;

        };

        input.CharacterControls.Look.canceled += ctx =>
        {
            lookMovement = Vector2.zero;
            Debug.Log(ctx.ReadValue<Vector2>());
        };

        input.CharacterControls.LockOn.performed += ctx =>
        {
            toggleLockOn();
        };

        rb = transform.GetComponent<Rigidbody>();
        powerUpParticles = GetComponentInChildren<ParticleSystem>();
        powerUpParticles.Stop();
        speedMultiplier = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        isRunningNHash = Animator.StringToHash("isRunningN");
        isStrafeNHash = Animator.StringToHash("isStrafeN");
        isStrafeEHash = Animator.StringToHash("isStrafeE");
        isStrafeSHash = Animator.StringToHash("isStrafeS");
        isStrafeWHash = Animator.StringToHash("isStrafeW");
        isLockedOn = Animator.StringToHash("isLockedOn");
        hasAttackedHash = Animator.StringToHash("hasAttacked");
        isJumpingHash = Animator.StringToHash("isJumping");
        isFallingHash = Animator.StringToHash("isFalling");
        hasInteractedHash = Animator.StringToHash("hasInteracted");

    }

    // Update is called once per frame
    void Update()
    {
        if(!cutsceneActive)
        {
            movement();
            handleMovement();
            handleAttack();
            
            
            handleJump();
        }
        groundCheck();
        handleGravity();
        handleInteract();

        if (defaultMovement)
        {
            rb.velocity = new Vector3(moveDir.x, playerMovement.y, moveDir.z);
            transform.forward = new Vector3(moveDir.x, 0, moveDir.z);
            
            
        }

        if(!defaultMovement)
        {
            rb.velocity = new Vector3(0, playerMovement.y, -playerMovement.z);
            transform.forward = new Vector3(0, 0, -playerMovement.z);
        }
        
    }

    //Script to toggle the lockOn state on or off
    void toggleLockOn()
    {
        lockedOn = !lockedOn;

        
        Debug.Log(lockedOn);
    }

    //movement script w/ jumping
    void movement()
    { 

        if (isGrounded)
        {
            speedMultiplier = speed;
        }
        if (!isGrounded)
        {
            speedMultiplier = speed;
        }

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 forwardRelative = playerMovement.z * camForward;
        Vector3 rightRelative = playerMovement.x * camRight;

        moveDir = forwardRelative + rightRelative;

        
    }

    void onJump(InputAction.CallbackContext context)
    {
        if(defaultMovement == true)
        {
            if (!IsGrounded() && jumpsRemaining >= maxJumps) return;
            if (jumpsRemaining == 0) StartCoroutine(WaitForLanding());
            jumpsRemaining++;
            velocity += jumpValue;
        }
    }

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);

        jumpsRemaining = 0;
    }

    void handleGravity()
    {
        if (isGrounded && velocity < 0.0f)
        {
            velocity = -1.0f;
            jumpsRemaining =-1 ;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        playerMovement.y = velocity;
    }

    void groundCheck()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, .5f, platformLayerMask))
        {
            isGrounded = false;

        }
        else
        {
            isGrounded = true;
            
        }
    }

    private bool IsGrounded() => isGrounded;

    //movement animator for both lockedOn and not lockedOn
    void handleMovement()
    {
        bool isRunningN = animator.GetBool(isRunningNHash);

        bool isStrafeN = animator.GetBool(isStrafeNHash);
        bool isStrafeE = animator.GetBool(isStrafeEHash);
        bool isStrafeS = animator.GetBool(isStrafeSHash);
        bool isStrafeW = animator.GetBool(isStrafeWHash);

        if (lockedOn)
        {
            animator.SetBool(isLockedOn, true);
        }

        if (!lockedOn)
        {
            animator.SetBool(isLockedOn, false);
        }

        if (!lockedOn)
        {
            if (movementPressed)
            {
                animator.SetBool(isRunningNHash, true);
            }

            if (!movementPressed)
            {
                animator.SetBool(isRunningNHash, false);
            }
        }

        if (lockedOn)
        {
            if (movementPressed)
            {
                if (currentMovement.y > currentMovement.x && currentMovement.x > 0)
                {
                    animator.SetBool(isStrafeNHash, true);
                    animator.SetBool(isStrafeEHash, false);
                    animator.SetBool(isStrafeSHash, false);
                    animator.SetBool(isStrafeWHash, false);
                }
                if (currentMovement.x > currentMovement.y && currentMovement.y > 0)
                {
                    animator.SetBool(isStrafeNHash, false);
                    animator.SetBool(isStrafeEHash, true);
                    animator.SetBool(isStrafeSHash, false);
                    animator.SetBool(isStrafeWHash, false);

                }
                if (currentMovement.y < currentMovement.x && currentMovement.y < 0)
                {
                    animator.SetBool(isStrafeNHash, false);
                    animator.SetBool(isStrafeEHash, false);
                    animator.SetBool(isStrafeSHash, true);
                    animator.SetBool(isStrafeWHash, false);
                }
                if (currentMovement.x < currentMovement.y && currentMovement.x < 0)
                {
                    animator.SetBool(isStrafeNHash, false);
                    animator.SetBool(isStrafeEHash, false);
                    animator.SetBool(isStrafeSHash, false);
                    animator.SetBool(isStrafeWHash, true);
                }
            }

            if (!movementPressed)
            {
                animator.SetBool(isStrafeNHash, false);
                animator.SetBool(isStrafeEHash, false);
                animator.SetBool(isStrafeSHash, false);
                animator.SetBool(isStrafeWHash, false);
        }
        
        }
        
    }

    void handleAttack()
    {
        bool hasAttacked = animator.GetBool(hasAttackedHash);
        
        if (!lockedOn)
        {
            if (attackPressed)
            {
                animator.SetBool(hasAttackedHash, true);
            }
            if (!attackPressed)
            {
                animator.SetBool(hasAttackedHash, false);
            }
        }
        
    }

    void handleInteract()
    {
        bool hasInteracted = animator.GetBool(hasInteractedHash);

            if (interactActive)
            {
                animator.SetBool(hasInteractedHash, true);
            }
            if (!interactActive)
            {
                animator.SetBool(hasInteractedHash, false);
            }
        

    }

    void handleJump()
    {
        bool hasJumped = animator.GetBool(isJumpingHash);
        bool isFalling = animator.GetBool(isFallingHash);

        if (defaultMovement)
        {
            if (!lockedOn)
            {

                if (jumpPressed)
                {
                    animator.SetBool(isJumpingHash, true);
                }


                if (isGrounded)
                {
                    animator.SetBool(isFallingHash, false);
                }

                if (!isGrounded)
                {
                    animator.SetBool(isJumpingHash, false);
                    animator.SetBool(isFallingHash, true);
                }
            }
        }
    }

    void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    void OnDisable()
    {
        input.CharacterControls.Disable();
    }
}
