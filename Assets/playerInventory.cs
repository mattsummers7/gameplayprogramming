using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public int NumberOfKeys { get; private set; }

    public void KeysCollected()
    {
        NumberOfKeys++;
    }
}
