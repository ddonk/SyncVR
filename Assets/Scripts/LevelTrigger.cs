using System;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private ObstacleType _obstacleType;
    
    private void OnTriggerEnter(Collider other)
    {
        //If our player triggers a game trigger handle the trigger depending on its type;
        if (!other.gameObject.tag.Equals("Player")) return;
        switch (_obstacleType)
        {
            case ObstacleType.End:
                LevelManager.Singleton.GenerateLane();
                break;
            case ObstacleType.Obstacle:
                UIManager.Singleton.HandleDeath();
                //We pause the game by stopping the characters movement by disabling the character controller.
                other.gameObject.TryGetComponent(out CharacterController characterController);
                characterController.enabled = false;
                break;
            case ObstacleType.Point:
                UIManager.Singleton.UpdateScore();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum ObstacleType
{
    Obstacle,
    //Triggerbox for when new tiles need to be loaded
    End,
    //To add a point to the score of the player.
    Point,
}