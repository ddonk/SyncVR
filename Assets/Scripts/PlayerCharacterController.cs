using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    public static CharacterController CharacterController;

    //Game object that blacks out the sides and the front of the game scene when moving.
    [SerializeField] private GameObject _cameraPanels;
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _accelerationTime;
    [SerializeField] private float _roadWidth;
    private Vector3 _velocity;
    private void Awake()
    {
        //Get the Character Controller attached to this gameobject
        if(!TryGetComponent(out CharacterController))
        {
            Debug.LogWarning("Couldnt find character controller");
        }

        //A coroutine that slowly increases the acceleration at which the player moves
        StartCoroutine(Accelerate());
    }
    
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        PlayerInput();
        //Move the player left or right depending on the player input
        CharacterController.Move(_velocity * _moveSpeed * Time.deltaTime);
        //Move the player forward
        CharacterController.Move(transform.forward * Time.deltaTime * _moveSpeed);
        
        //Clamp the position of the player on the x axis so the player cant go out of bounds.
        Vector3 pos = transform.position;
        transform.position = new Vector3(Mathf.Clamp(pos.x, -_roadWidth, _roadWidth),pos.y , pos.z);
        
        //Move the camera panels with the player, cant be a child of the object. Then panels will move left and right then which we dont want.
        _cameraPanels.transform.position = new Vector3(_cameraPanels.transform.position.x , _cameraPanels.transform.position.y, transform.position.z);
    }

    private void PlayerInput()
    {
        var horInput = Input.GetAxis("Horizontal");
        
        //Get the relative x axis. So when player is rotated is still moves left and right relative to what is the players forward
        var relativeX = transform.TransformDirection(transform.position);
        
        //Go back to the center of the lane if no inputs are given
        if (!Input.GetKey(KeyCode.A) && relativeX.x < 0.00f)
        {
            _velocity = transform.right;
        } else if (!Input.GetKey(KeyCode.D) && relativeX.x > 0.00f)
        {
            _velocity = -transform.right;
        }
        else
        {
            _velocity = horInput * transform.right;
        }
    }
    
    private IEnumerator Accelerate()
    {
        while (true)
        {
            //Avoids accelerations when game is paused
            if (CharacterController.enabled)
            {
                _moveSpeed += _accelerationSpeed;
            }

            yield return new WaitForSeconds(_accelerationTime);
        }
    } 
}