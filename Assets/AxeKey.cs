using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeKey : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        playerInventory PlayerInventory = other.GetComponent<playerInventory>();

        if (PlayerInventory != null)
        {
            PlayerInventory.KeysCollected();
            gameObject.SetActive(false);
        }
    }
}
