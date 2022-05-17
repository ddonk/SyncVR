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
    [SerializeField] private GameObject levelObject;
    [SerializeField] private float xTileSize;

    private Vector3 _initTilePos;
    private Vector3 _initTileRot;
    public void GenerateLane()
    {
        Debug.Log(currentTiles.Count);
        
        for (int i = 0; i < currentTiles.Count-3; i++)
        {
            Destroy(currentTiles[i]);
        }
        
        currentTiles.Clear();

        for (int i = 0; i < 6; i+=2)
        {
            InitTile(tilesPrefabs[0], Singleton._initTilePos, Singleton._initTileRot);
            _initTilePos.z += xTileSize;
            InitTile(tilesPrefabs[RandomRangeInt(1, tilesPrefabs.Count-2)], Singleton._initTilePos, Singleton._initTileRot);
            _initTilePos.z += xTileSize;
        }
        InitTile(tilesPrefabs[0], _initTilePos, Singleton._initTileRot);
        InitTile(tilesPrefabs[tilesPrefabs.Count-1], new Vector3(Singleton._initTilePos.x, Singleton._initTilePos.y, Singleton._initTilePos.z - (xTileSize)), Singleton._initTileRot);
    }

    private int RandomRangeInt(int min, int max)
    {
        return Random.Range(min, max);
    }

    private void InitTile(GameObject prefab, Vector3 pos, Vector3 rot)
    {
        GameObject gameObject = Instantiate(prefab, pos, Quaternion.Euler(rot));
        gameObject.transform.parent = levelObject.transform;
        currentTiles.Add(gameObject);
    }
    
    private void Awake()
    {
        if (Singleton != null && Singleton != this) 
        { 
            Destroy(this); 
        } 
        else 
        {
            Debug.Log("Init Level Manager Singleton");
            _initTilePos = new Vector3(0,0,8);
            _initTileRot = new Vector3(0, 0, 0);
            Singleton = this;
        }
        
        Singleton.GenerateLane();
    }
}
