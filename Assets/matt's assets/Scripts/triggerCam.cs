using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCam : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    public characterMovement player;
    public bool characterMovementEnabled;

    private void OnTriggerEnter(Collider other)
    {
        cam2.SetActive(true);
        cam1.SetActive(false);

        player.defaultMovement = characterMovementEnabled;
    }
}
