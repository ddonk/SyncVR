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
                GenerateNewLevel(other.gameObject);
                break;
            case ObstacleType.OBSTACLE:
                Dead(other.gameObject);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Dead(GameObject _gameObject)
    {
        Destroy(_gameObject);
    }
    
    private void GenerateNewLevel(GameObject _gameObject)
    {
        _gameObject.transform.Rotate(0,90,0);
        LevelManager.Singleton.GenerateLane();
    }
}

public enum ObstacleType
{
    OBSTACLE,
    TSPLIT,
}
