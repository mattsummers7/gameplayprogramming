using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerDoor : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 5;

    public bool startDoorOpen;

    void Start()
    {
        transform.position = pointA.position;
        speed = speed * Time.deltaTime;


    }

    void Update()
    {
        if (startDoorOpen)
        {
            StartCoroutine(DoorOpen());
        }
    }


    IEnumerator DoorOpen()
    {
        while (transform.position != pointB.position)
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, pointB.position, speed);
            yield return new WaitForEndOfFrame();
        }
    }
}
