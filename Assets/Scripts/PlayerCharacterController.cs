using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    public static CharacterController _characterController;

    [SerializeField] private GameObject cameraPanels;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float accelerationTime;
    [SerializeField] private float roadWidth;
    private Vector3 velocity;
    private void Awake()
    {
        if(!TryGetComponent(out _characterController))
        {
            Debug.LogWarning("Couldnt find character controller");
        }

        StartCoroutine(Accelerate());
    }

    private void Update()
    {
        PlayerInput();
        _characterController.Move(velocity * moveSpeed * Time.deltaTime);
        _characterController.Move(transform.forward * Time.deltaTime * moveSpeed);
        
        Vector3 pos = transform.position;
        transform.position = new Vector3(Mathf.Clamp(pos.x, -roadWidth, roadWidth),pos.y , pos.z);
        cameraPanels.transform.position = new Vector3(cameraPanels.transform.position.x , cameraPanels.transform.position.y, transform.position.z);
    }

    private void PlayerInput()
    {
        var horInput = Input.GetAxis("Horizontal");
        var relativeX = transform.TransformDirection(transform.position);
        
        if (!Input.GetKey(KeyCode.A) && relativeX.x < 0.00f)
        {
            velocity = transform.right;
        } else if (!Input.GetKey(KeyCode.D) && relativeX.x > 0.00f)
        {
            velocity = -transform.right;
        }
        else
        {
            velocity = horInput * transform.right;
        }
    }
    private IEnumerator Accelerate()
    {
        while (true)
        {
            if (_characterController.enabled)
            {
                moveSpeed += accelerationSpeed;
            }

            yield return new WaitForSeconds(accelerationTime);
        }
    } 
}
