using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;

    public void NegativeHealth(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            SceneManager.LoadScene(2);
        }    
        
    }


}
