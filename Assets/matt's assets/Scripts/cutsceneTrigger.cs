using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutsceneTrigger : MonoBehaviour
{
    PlayerInput input;


    public characterMovement player;
    public buttonDown buttonPush;
    public triggerDoor doorOpen;

    public GameObject thePlayer;
    public GameObject cutsceneCam;


    
    bool interactPressed;
    bool pushButton = false;

    void Start()
    {
        
    }

    void Awake()
    {
        input = new PlayerInput();

        input.CharacterControls.Interact.performed += ctx =>
        {
            interactPressed = ctx.ReadValueAsButton();
        };

    }

    void Update()
    {
        player.interactActive = pushButton;
    }

    void OnTriggerStay(Collider other)
    {
        

        if(interactPressed)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            
            cutsceneCam.SetActive(true);
            thePlayer.SetActive(false);
            player.transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z - 1));
            
            StartCoroutine(FinishCut());
        }
        
    }

    IEnumerator FinishCut()
    {
        player.cutsceneActive = true;
        yield return new WaitForSeconds(0.5f);
        pushButton = true;
        yield return new WaitForSeconds(0.14f);
        buttonPush.startButtonPush = true;

        yield return new WaitForSeconds(2.5f);
        doorOpen.startDoorOpen = true;
        yield return new WaitForSeconds(2);
        pushButton = false;
        thePlayer.SetActive(true);
        cutsceneCam.SetActive(false);
        player.cutsceneActive = false;
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
