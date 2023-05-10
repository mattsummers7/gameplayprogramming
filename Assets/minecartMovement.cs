using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class minecartMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public characterMovement disableMovement;
    public float speed = 1f;
    bool isGrounded;
    bool isMoving;
    bool canJump;
    public float jumpForce = 20;
    public float gravity = -9.81f;
    public float gravityScale = 5;
    float velocity;

    PlayerInput input;

    void Awake()
    {
        input = new PlayerInput();

        input.CharacterControls.Jump.started += onJump;

    }

    // Update is called once per frame
    void Update()
    {
        groundCheck();
        handleGravity();
        transform.Translate((Vector3.forward * speed) * Time.deltaTime);

        if(canJump)
        {
            transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);
        }

        if (isMoving)
        {
            StartCoroutine(MoveCart());
        }
        
    }

    void onJump(InputAction.CallbackContext context)
    {
        if(isGrounded)
        {
            velocity += jumpForce;
        }
        
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
            Debug.Log("true");

        }
    }

    void handleGravity()
    {
        if (isGrounded && velocity < 0.0f)
        {
            velocity = 0;
        }
        else
        {
            velocity += gravity * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        isMoving = true;

        
        

    }

    IEnumerator MoveCart()
    {
        /*this.gameObject.GetComponent<BoxCollider>().enabled = false;*/
        disableMovement.defaultMovement = false;
        isMoving = false;
        speed = 6;
        canJump = true;
        
        yield return new WaitForSeconds(3);

        Debug.Log(isMoving);

        speed = 0;
        canJump = false;
        disableMovement.defaultMovement = true;


        yield return new WaitForSeconds(1);
        

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
