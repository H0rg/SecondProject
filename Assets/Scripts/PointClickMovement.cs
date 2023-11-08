using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PointClickMovement : MonoBehaviour
{
    [Header("Movement")]
    //[SerializeField] private Transform target;
    private CharacterController _characterController;
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float rotSpeed = 15.0f;
    [Header("Jump")]
    [SerializeField] private float jumpSpeed = 15.0f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float terminalVelocity = -10.0f;
    [SerializeField] private float minFall = -1.5f;
    
    [SerializeField] private float pushForce = 15.0f;

    [Header("PointClick")] 
    [SerializeField] private float deceleration = 5.0f;
    [SerializeField] private float targetBuffer = 1.5f;
    private float _curSpeed = 0f;
    private Vector3 _targetPos = Vector3.one;
    
    private Animator _animator;
    private ControllerColliderHit _contact;
    [SerializeField] private Camera camera;
    
    private float _vertSpeed;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _vertSpeed = minFall;
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetMouseButton(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(ray, out mouseHit))
            {
                _targetPos = mouseHit.point;
                _curSpeed = moveSpeed;
            }
        }

        if (_targetPos != Vector3.one)
        {
            if (_curSpeed > moveSpeed * .5f)
            {
                Vector3 adjustedPos = new Vector3(_targetPos.x, transform.position.y, _targetPos.z);
                Quaternion targetRot = Quaternion.LookRotation(adjustedPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            }

            movement = _curSpeed * Vector3.forward;
            movement = transform.TransformDirection(movement);
            if (Vector3.Distance(_targetPos, transform.position) < targetBuffer)
            {
                _curSpeed = deceleration * Time.deltaTime;
                if (_curSpeed <= 0)
                {
                    _targetPos = Vector3.one;
                }
                
            }
        }
        _animator.SetFloat("Speed", movement.sqrMagnitude);
        
        // bool hitGround = false;
        // RaycastHit hit;
        // if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        // {
        //     float check = (_characterController.height + _characterController.radius) / 1.9f;
        //     hitGround = hit.distance <= check;
        // }
        // if (hitGround)
        // {
        //     if (Input.GetButtonDown("Jump"))
        //         _vertSpeed = jumpSpeed;
        //     else
        //     {
        //         _vertSpeed = minFall;
        //         _animator.SetBool("Jumping", false);
        //     }
        // }
        // else
        // {
        //     _vertSpeed += gravity * 5 * Time.deltaTime;
        //     if (_vertSpeed < terminalVelocity)
        //         _vertSpeed = terminalVelocity;
        //     if (_contact != null)
        //     {
        //         _animator.SetBool("Jumping", true);
        //     }
        //     if (_characterController.isGrounded)
        //     {
        //         if (Vector3.Dot(movement, _contact.normal) < 0)
        //         {
        //             movement = -_contact.normal * moveSpeed;
        //         }
        //         else
        //         {
        //             movement += _contact.normal * moveSpeed;
        //         }
        //     }
        // }
        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _characterController.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
            Debug.Log("HIT YOU ");
        } 
    }
}
