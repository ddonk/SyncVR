using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Singleton { get; private set; }
    [SerializeField] private List<GameObject> tilesPrefabs;
    [SerializeField] private List<GameObject> currentTiles;

    [SerializeField] private float xTileSize;

    private Vector3 _initTilePos;
    private Vector3 _initTileRot;
    public void GenerateLane()
    {
        for (int i = 0; i < currentTiles.Count-3; i++)
        {
            Destroy(currentTiles[i]);
        }
        
        currentTiles.Clear();

        for (int i = 0; i < 6; i+=2)
        {
            currentTiles.Add(Instantiate(tilesPrefabs[0], _initTilePos, Quaternion.Euler(Singleton._initTileRot)));
            _initTilePos.z += xTileSize;
            currentTiles.Add(Instantiate(tilesPrefabs[RandomRangeInt(1, tilesPrefabs.Count-2)], _initTilePos, Quaternion.Euler(Singleton._initTileRot)));
            _initTilePos.z += xTileSize;
        }
        currentTiles.Add(Instantiate(tilesPrefabs[0], _initTilePos, Quaternion.Euler(Singleton._initTileRot)));
        currentTiles.Add(Instantiate(tilesPrefabs[tilesPrefabs.Count-1], _initTilePos, Quaternion.Euler(Singleton._initTileRot)));
    }

    private int RandomRangeInt(int min, int max)
    {
        return Random.Range(min, max);
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
            Singleton._initTilePos = new Vector3();
            Singleton._initTileRot = new Vector3(0, 90, 0);
            Singleton.currentTiles = new List<GameObject>();
            Singleton.xTileSize = xTileSize;
        }
    }
}
