using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    [SerializeField] float period = 2f;
    
    private float movementFactor;
    private Vector3 startingPos;
    private Vector3 offSet;
    private const float tau = Mathf.PI * 2;
    private float cycles;
    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        if (period > 0)
        {
            MoveSin();
        }
    }

    private void MoveSin()
    {
        cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1f) / 2;

        offSet = movementVector * movementFactor;
        transform.position = startingPos + offSet;
    }
}
