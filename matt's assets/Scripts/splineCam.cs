using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splineCam : MonoBehaviour
{

    public GameObject target;
    // Start is called before the first frame update
    void LateUpdate()
    {
        transform.LookAt(target.transform);
    }
}
