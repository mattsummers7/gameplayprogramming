using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShot : MonoBehaviour
{
    Rigidbody rig;

    [SerializeField]
    float speedShot = 1000f;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = target.position - transform.position;
        rig.AddForce(direction * speedShot * Time.deltaTime);

    }



}
