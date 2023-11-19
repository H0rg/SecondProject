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
    
    [Header("Particles")]
    [SerializeField] public ParticleSystem _mainBusterParticles;
    [SerializeField] public ParticleSystem _leftBusterParticle;
    [SerializeField] public ParticleSystem _rightBusterParticle;
    
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    
    private bool thrusting;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
        RocketSound();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) 
            StartThrust();
        else 
            StopThrust();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A)) 
            RotationRight();
        else 
            _rightBusterParticle.Stop();
        
        if (Input.GetKey(KeyCode.D)) 
            RotationLeft();
        else 
            _leftBusterParticle.Stop();
    }

    private void RocketSound()
    {
        if (thrusting)
        {
            if (!_audioSource.isPlaying)
                _audioSource.PlayOneShot(_thrustClip); 
        }
        else _audioSource.Stop();
    }

    private void StartThrust()
    {
        thrusting = true;
        if (!_mainBusterParticles.isPlaying)
            _mainBusterParticles.Play();
        _rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    private void StopThrust()
    {
        thrusting = false;
        _mainBusterParticles.Stop();
    }

    private void RotationRight()
    {
        if (!_rightBusterParticle.isPlaying)
            _rightBusterParticle.Play();
        ApplyRotation(rotationThrust);
    }

    private void RotationLeft()
    {
        if (!_leftBusterParticle.isPlaying)
            _leftBusterParticle.Play();
        ApplyRotation(-rotationThrust);
    }


    void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }

}
