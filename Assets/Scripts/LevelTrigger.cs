using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private ObstacleType _obstacleType;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player")) return;
        switch (_obstacleType)
        {
            case ObstacleType.TSPLIT:
                LevelManager.Singleton.GenerateLane();
                break;
            case ObstacleType.OBSTACLE:
                UIManager.Singleton.HandleDeath();
                other.gameObject.TryGetComponent<CharacterController>(out CharacterController characterController);
                characterController.enabled = false;
                break;
            case ObstacleType.POINT:
                UIManager.Singleton.UpdateScore();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum ObstacleType
{
    OBSTACLE,
    TSPLIT,
    POINT,
}
