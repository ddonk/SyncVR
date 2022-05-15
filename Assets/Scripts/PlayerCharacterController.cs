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

        velocity.z = moveSpeed;
    }

    private void Update()
    {
        PlayerInput();
        _characterController.Move(velocity * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -roadWidth, roadWidth), transform.position.y, transform.position.z);
    }

    private void PlayerInput()
    {
        var horizontal = Input.GetAxis("Horizontal");

        velocity.x = horizontal switch
        {
            0 when transform.position.x > 0 => -moveSpeed,
            0 when transform.position.x < 0 => moveSpeed,
            _ => horizontal * moveSpeed
        };
    }
}
