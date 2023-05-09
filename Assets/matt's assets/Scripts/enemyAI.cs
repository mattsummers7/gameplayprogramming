using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public float idleSpeed = 30f;
    public float distance;
    public float health;

    private Coroutine slimeUpdate;
    private Rigidbody rb;
    public GameObject player;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        slimeUpdate = StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        float directionx = 1 * Time.deltaTime;
        float directiony = 1 * Time.deltaTime;

        while (true)
        {
            rb.velocity = (new Vector3(idleSpeed * directionx, 0, idleSpeed * directiony));

            directionx *= -1;
            directiony *= -1;

            yield return new WaitForSeconds(2);
        }
    }

    void Update()
    {
        getDistance();
        if (distance < 10)
        {
            Debug.Log("player is close");
        }
        
    }

    void getDistance()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
    }

    private void DestroyEnemy()
    {

    }





}
