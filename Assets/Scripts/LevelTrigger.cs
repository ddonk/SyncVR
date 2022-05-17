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
                LevelManager.Singleton.GenerateLane();
                break;
            case ObstacleType.OBSTACLE:
                Dead(other.gameObject);
                break;
            case ObstacleType.POINT:
                UIManager.Singleton.UpdateScore();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Dead(GameObject _gameObject)
    {
        Destroy(_gameObject);
    }
}

public enum ObstacleType
{
    OBSTACLE,
    TSPLIT,
    POINT,
}
