using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed = 3f;


    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        float xValue = Input.GetAxis("Horizontal");
        float zValue = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(xValue, 0, zValue);
        direction = Vector3.ClampMagnitude(direction, 1);
        
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
