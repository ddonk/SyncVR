using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private ObstacleType _obstacleType;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player")) return;
        switch (_obstacleType)
        {
            case ObstacleType.TSPLIT:
                GenerateNewLevel();
                break;
            case ObstacleType.OBSTACLE:
                Dead(other.gameObject);
                break;
            case ObstacleType.POINT:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Dead(GameObject _gameObject)
    {
        Destroy(_gameObject);
    }
    
    private void GenerateNewLevel()
    {
        LevelManager.Singleton.GenerateLane();
    }
}

public enum ObstacleType
{
    OBSTACLE,
    TSPLIT,
    POINT,
}
