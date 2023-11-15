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
        isAlive = false;
        _movement.enabled = false;
        _audio.Stop();
        _audio.PlayOneShot(_crushClip);
        Invoke("ReloadLevel",deathDuration);
    }

    void StartSuccessSequence()
    {
        _movement.enabled = false;
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;
        _rigidbody.velocity = Vector3.zero;
        _audio.Stop();
        _audio.PlayOneShot(_successClip);
        Invoke("LoadNextLevel",finishDuration);
    }

    private void LoadNextLevel()
    {
        int allScene = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        Debug.Log($"You finish level {currentSceneIndex + 1}");
        if (currentSceneIndex == allScene - 1)
        {
            Debug.Log("You finish The Game)");
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
}
