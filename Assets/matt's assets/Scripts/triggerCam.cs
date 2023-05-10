using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCam : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    public characterMovement player;

    private void OnTriggerEnter(Collider other)
    {
        cam2.SetActive(true);
        cam1.SetActive(false);

    }
}
