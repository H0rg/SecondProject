using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDropper : MonoBehaviour
{
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
