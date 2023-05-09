using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotator : MonoBehaviour
{
    public float speed;
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.02f);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, 0.01f);
    }
}
