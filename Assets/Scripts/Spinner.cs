using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 2f;
    void Update()
    {
        transform.Rotate(0,spinSpeed * Time.deltaTime, 0);
    }
}
