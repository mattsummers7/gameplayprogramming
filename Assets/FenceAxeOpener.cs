using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceAxeOpener : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        playerInventory PlayerInventory = other.GetComponent<playerInventory>();

        if (PlayerInventory != null)
        {
            if(PlayerInventory.NumberOfKeys == 3)
            {
                gameObject.SetActive(false);
                Debug.Log("opened");
            }
            else
            {
                gameObject.SetActive(true);
                Debug.Log("not opened");
            }
        }
    }
}
