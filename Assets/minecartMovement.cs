using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class minecartMovement : MonoBehaviour
{
    public characterMovement jumpPressed;
    public float speed = 1f;
    bool isGrounded;
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


        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);

    }

    void onJump(InputAction.CallbackContext context)
    {
        velocity += gravity * gravityScale * Time.deltaTime;
    }

    void groundCheck()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, .5f))
        {
            isGrounded = false;

        }
        else
        {
            isGrounded = true;

        }
    }
    void handleGravity()
    {
        if (isGrounded && velocity < 0.0f)
        {
            velocity = -1.0f;

        }
        else
        {
            velocity += gravity * gravityScale * Time.deltaTime;
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
