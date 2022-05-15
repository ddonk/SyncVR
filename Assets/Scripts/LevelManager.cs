using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Singleton { get; private set; }
    [SerializeField] private List<GameObject> tilesPrefabs;
    private List<GameObject> currentTiles;

    private Vector3 _initTilePos;
    private Vector3 rotation;

    public void GenerateLane()
    {
        foreach (var tile in currentTiles)
        {
           Destroy(tile); 
        }
        
        currentTiles.Clear();
        
        for (int i = 0; i < 3; i++)
        {
            currentTiles.Add(Instantiate(tilesPrefabs[0], _initTilePos, Quaternion.Euler(rotation)));
            ChangeInitPos();
        }
        
        currentTiles.Add(Instantiate(tilesPrefabs[2], _initTilePos, Quaternion.Euler(rotation)));
        ChangeInitRotation();
    }

    private void ChangeInitPos()
    {
        switch (rotation.y)
        {
            case 0:
                Singleton._initTilePos.z += 10;
                break;
            case 90:
                Singleton._initTilePos.x += 10;
                break;
            case 180:
                Singleton._initTilePos.z -= 10;
                break;
            case 270:
                Singleton._initTilePos.x -= 10;
                break;
        }
    }

    private void ChangeInitRotation()
    {
        if (Singleton.rotation.y.Equals(0))
        {
            Singleton.rotation.y += 90;
            Singleton._initTilePos.x += 10;
            Singleton._initTilePos.z += 13.75f;
        } else if (Singleton.rotation.y.Equals(90))
        {
            Singleton.rotation.y += 90;
            Singleton._initTilePos.x += 13.75f;
            Singleton._initTilePos.z -= 10f;
        } else if (Singleton.rotation.y.Equals(180))
        {
            Singleton.rotation.y += 90;
            Singleton._initTilePos.x -= 10;
            Singleton._initTilePos.z -= 13.75f;
        } else if (Singleton.rotation.y.Equals(270))
        {
            Singleton.rotation.y = 0;
            Singleton._initTilePos.x = 0;
            Singleton._initTilePos.z = 0;
        }
    }
    
    private void Awake()
    {
        if (Singleton != null && Singleton != this) 
        { 
            Destroy(this); 
        } 
        else 
        {
            Debug.Log("Init singleton");
            Singleton = this;
            Singleton.tilesPrefabs = tilesPrefabs;
            Singleton._initTilePos = _initTilePos;
            Singleton.currentTiles = new List<GameObject>();
        }
    }
}
