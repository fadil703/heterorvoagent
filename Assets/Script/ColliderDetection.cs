using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    
    // Use this for initialization
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collide detect");
    }
    
}
