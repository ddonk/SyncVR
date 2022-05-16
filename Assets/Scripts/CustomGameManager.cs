using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomGameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelManager.Singleton.GenerateLane();
        }
    }
}
