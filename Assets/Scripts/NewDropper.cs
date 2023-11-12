using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDropper : MonoBehaviour
{
    private float time = 0f;
    [SerializeField] private float timeToWait = 3f;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        Fall();
    }

    private void Fall()
    {
        _meshRenderer.enabled = true;
        _rigidbody.useGravity = true;
    }
}
