using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    private int score = 0;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Ground"))
        {
            if (!other.collider.CompareTag("Hitted"))
            {
                other.collider.GetComponent<MeshRenderer>().material.color = Color.red;
                other.collider.tag = "Hitted";
                score++; 
                Debug.Log($"You hit something this many times: {score}");
            }
        }
    }
}
