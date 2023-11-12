using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    private Color color;
 

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("You hit the wall");
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
