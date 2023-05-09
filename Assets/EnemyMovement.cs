using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;

    public float velocity = 5f;

    public float stoppingDistance = 1f;

    public float retreatDistance = 2f;

    private Vector3 initialPosition;

    private bool isComingBack = false;

    private bool isPlayerInRange = false;

    private void Start()
    {
        initialPosition = transform.position;
        target = GameObject.FindWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            if (Vector3.Distance(transform.position, target.position) > stoppingDistance && !isComingBack)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, velocity * Time.deltaTime);
            }
            else if (Vector3.Distance(transform.position, target.position) < stoppingDistance && Vector3.Distance(transform.position, target.position) > retreatDistance && !isComingBack)
            {
                transform.position = this.transform.position;
            }
            else if (Vector3.Distance(transform.position, target.position) < retreatDistance || isComingBack)
            {
                isComingBack = true;
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, velocity * Time.deltaTime);

                if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
                {
                    isComingBack = false;
                }

            }
        }
    }
}
