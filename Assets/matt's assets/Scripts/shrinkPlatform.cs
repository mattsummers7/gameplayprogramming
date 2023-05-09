using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostPlatforms : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] float disappearTime = 5;
    Animator anim;

    [SerializeField] bool canReset;
    [SerializeField] float resetTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("disappearTime", 1 / disappearTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == playerTag)
        {
            anim.SetBool("trigger", true);
        }
    }

    public void TriggerReset()
    {
        if(canReset)
        {
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        anim.SetBool("trigger", false);
    }

}
