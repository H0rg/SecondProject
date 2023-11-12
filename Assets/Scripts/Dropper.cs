using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    private float time = 0f;
    [SerializeField] private float timeToWait = 3f;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        time = Time.time;
        if (time >= timeToWait)
        {
            Fall();
        }
    }

    private void Fall()
    {
        _meshRenderer.enabled = true;
        _rigidbody.useGravity = true;
    }
}
