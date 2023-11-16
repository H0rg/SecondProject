using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float deathDuration = 3f;
    [SerializeField] private float finishDuration = 2f;
    [SerializeField] private AudioClip _successClip;
    [SerializeField] private AudioClip _crushClip;
    [SerializeField] private AudioClip _finishGameClip;
    [SerializeField] private ParticleSystem _successParticles;
    [SerializeField] private ParticleSystem _crushParticles;

    private Movement _movement;
    private AudioSource _audio;
    private Rigidbody _rigidbody;

    private bool isAlive = true;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _audio = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DebugTools();
    }

    private void DebugTools()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isAlive = !isAlive;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isAlive)
        {
            switch (other.collider.tag)
            {
                case "Friendly":
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    void StartCrashSequence()
    {
        StopMovementParticles();
        _crushParticles.Play();
        isAlive = false;
        _movement.enabled = false;
        _audio.Stop();
        _audio.PlayOneShot(_crushClip);
        Invoke("ReloadLevel", deathDuration);
    }

    void StartSuccessSequence()
    {
        StopMovementParticles();
        _successParticles.Play();
        FrezzeMovementAndRotation();
        _audio.Stop();
        _audio.PlayOneShot(_successClip);
        Invoke("LoadNextLevel", finishDuration);
    }

    private void LoadNextLevel()
    {
        int allScene = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        print($"You finish level {currentSceneIndex + 1}");
        if (currentSceneIndex == allScene - 1)
        {
            print("You finish The Game)");
            currentSceneIndex = 0;
            SceneManager.LoadScene(currentSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void FrezzeMovementAndRotation()
    {
        _movement.enabled = false;
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;
        _rigidbody.velocity = Vector3.zero;
    }

    private void StopMovementParticles()
    {
        _movement._mainBusterParticles.Stop();
        _movement._leftBusterParticle.Stop();
        _movement._rightBusterParticle.Stop();
    }
}
