using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DangerousArea : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private void Start()
    {
        _gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameObject.SetActive(true);
        }
    }
}
