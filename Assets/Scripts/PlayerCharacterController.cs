using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float roadWidth;
    private Vector3 velocity;
    private void Awake()
    {
        if(!TryGetComponent(out _characterController))
        {
            Debug.LogWarning("Couldnt find character controller");
        }
    }

    private void Update()
    {
        PlayerInput();
        _characterController.Move(velocity * moveSpeed * Time.deltaTime);
        _characterController.Move(transform.forward * Time.deltaTime * moveSpeed);
    }

    private void PlayerInput()
    {
        var horInput = Input.GetAxis("Horizontal");
        var relativeX = transform.TransformDirection(transform.position);
        
        if (!Input.GetKey(KeyCode.A) && relativeX.x < -0.05f)
        {
            velocity = transform.right;
        } else if (!Input.GetKey(KeyCode.D) && relativeX.x > 0.05f)
        {
            velocity = -transform.right;
        }
        else
        {
            velocity = horInput * transform.right;
        }
    }
}
