using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float rotationThrust = 70f;
    [SerializeField] private float mainThrust = 1000f;
    
    [Header("AudioClips")]
    [SerializeField] private AudioClip _thrustClip;
    
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    
    private bool thrusting;
    private float zRotate = 1;

    private void Start()
    {
        Debug.Log("Respawn");
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        RocketSound();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            thrusting = true;
            _rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else
        {
            thrusting = false;
        }
    }

    void RocketSound()
    {
        if (thrusting)
        {
            if (!_audioSource.isPlaying)
                _audioSource.PlayOneShot(_thrustClip);
        }
        else
        {
            _audioSource.Stop();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }

}
