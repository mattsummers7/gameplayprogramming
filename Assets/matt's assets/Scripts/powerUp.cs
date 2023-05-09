using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    public characterMovement playerVariables;
    public Renderer rend;
    public Collider mesh;

    float spinSpeed = 50;
    float speedMultiplier = 2;
    public float timerDuration;
    bool powerUpActive;
    

    //look in powerUpEffect to see which integer will grant which powerup - set up the powerup in the inspector
    public int powerUpType;
    

    void Start()
    {
        rend = GetComponent<Renderer>();
        mesh = GetComponent<Collider>();
        rend.enabled = true;
        mesh.enabled = true;
        

        if (powerUpType == 1)
        {
            rend.material.color = Color.green;
            
        }

        if (powerUpType == 2)
        {
            rend.material.color = Color.blue;
            
        }
    }

    void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime, Space.Self);
    }  

    void particleColour(float r, float g, float b, float a)
    {
        Vector4 color = new Vector4(r, g, b, a);
    }

    void powerUpEffect()
    {
        //speed boost
        var main = playerVariables.powerUpParticles.main;
        if (powerUpActive)
        {
            if (powerUpType == 1)
            {
                playerVariables.speed *= speedMultiplier;
                main.startColor = new Color(0, 100, 0, 100);
            }
        }
        if (!powerUpActive)
        {
            if (powerUpType == 1)
            {
                playerVariables.speed /= 2 ;
                
            }
        }


        //extra jumps
        if (powerUpActive)
        {
            if (powerUpType == 2)
            {
                playerVariables.maxJumps++;
                main.startColor = new Color(0, 0, 100, 100);
            }
        }
        if (!powerUpActive)
        {
            if(powerUpType == 2)
            {
                playerVariables.maxJumps = 0;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if(other == PlayerCollider)
        {
            
            StartCoroutine(PowerUp(timerDuration));
            //DestroyObject();

        }
        
    }

    void DestroyObject()
    {
        if (powerUpActive)
        {
            rend.enabled = false;
            mesh.enabled = false;
        }

        if (!powerUpActive)
        {
            rend.enabled = true;
            mesh.enabled = true;
        }

    }

    IEnumerator PowerUp (float duration)
    {
        Debug.Log("Timer Started");

        powerUpActive = true;
        Debug.Log(powerUpActive);
        powerUpEffect();
        DestroyObject();
        playerVariables.powerUpParticles.Play();

        yield return new WaitForSeconds(duration);

        powerUpActive = false;
        Debug.Log(powerUpActive);
        powerUpEffect();
        DestroyObject();
        playerVariables.powerUpParticles.Stop();

        Debug.Log("power up ended");
    }
}
