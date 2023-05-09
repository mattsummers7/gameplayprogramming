using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class followCamera : MonoBehaviour
{
    PlayerInput input;

    Vector2 lookMovement;
    bool lookPressed;

    public GameObject target;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void Awake()
    {
        input = new PlayerInput();

        input.CharacterControls.Look.performed += ctx =>
        {
            lookMovement = ctx.ReadValue<Vector2>();

            lookPressed = lookMovement.x != 0 || lookMovement.y != 0;

        };

        input.CharacterControls.Look.canceled += ctx =>
        {
            lookMovement = Vector2.zero;
        };
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float angleBetween = Vector3.Angle(Vector3.up, transform.forward);


        if(lookPressed)
        {
            offset = Quaternion.Euler(0, lookMovement.x, 0) * offset;
            Vector3 LocalRight = target.transform.worldToLocalMatrix.MultiplyVector(transform.right);
            if (((angleBetween > 80.0f) && (lookMovement.y < 0)) || ((angleBetween < 140.0f) && (lookMovement.y > 0)))
            {
                offset = Quaternion.AngleAxis(lookMovement.y, LocalRight) * offset;
            }
            
        }

        /*float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);*/
        transform.position = target.transform.position + offset;
        transform.LookAt(target.transform);
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
