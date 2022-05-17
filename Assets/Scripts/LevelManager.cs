using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Singleton { get; private set; }
    [SerializeField] private List<GameObject> _tilesPrefabs;
    [SerializeField] private List<GameObject> _currentTiles;
    [SerializeField] private GameObject _levelObject;
    [SerializeField] private float _xTileSize;

    private Vector3 _initTilePos;
    private Vector3 _initTileRot;
    
    //Numer magic: Element 0 is always a non-obstacle tile gameobject and the last element is always the triggerbox gameobject to load in new tiles.
    public void GenerateLane()
    {
        //Cleanup step removing older tiles. We do not remove the last 3 elements because you can still see them when adding new tiles.
        for (int i = 0; i < _currentTiles.Count-3; i++)
        {
            Destroy(_currentTiles[i]);
        }
        
        //Instantiate 6 tiles, were alternately one has a random obstacle and one doesnt.
        for (int i = 0; i < 6; i+=2)
        {
            InitTile(_tilesPrefabs[0], Singleton._initTilePos, Singleton._initTileRot);
            _initTilePos.z += _xTileSize;
            InitTile(_tilesPrefabs[RandomRangeInt(1, _tilesPrefabs.Count-2)], Singleton._initTilePos, Singleton._initTileRot);
            _initTilePos.z += _xTileSize;
        }
        
        //Instantiate a non-obstacle tile and a trigger box to load in new tiles when reaching the end of the tiles.
        InitTile(_tilesPrefabs[0], _initTilePos, Singleton._initTileRot);
        InitTile(_tilesPrefabs[_tilesPrefabs.Count-1], new Vector3(Singleton._initTilePos.x, Singleton._initTilePos.y, Singleton._initTilePos.z - (_xTileSize)), Singleton._initTileRot);
    }
    
    private int RandomRangeInt(int min, int max)
    {
        return Random.Range(min, max);
    }
    
    
    private void InitTile(GameObject prefab, Vector3 pos, Vector3 rot)
    {
        GameObject instantiate = Instantiate(prefab, pos, Quaternion.Euler(rot));
        //Set all tiles and triggerboxes under one gameobject to keep gamescene clean.
        instantiate.transform.parent = _levelObject.transform;
        _currentTiles.Add(instantiate);
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
            //We have four starting tiles so when start instantiating tiles on the fourth tile.
            _initTilePos = new Vector3(0,0,4*_xTileSize);
            _initTileRot = new Vector3(0, 0, 0);
            Singleton = this;
        }
        
        Singleton.GenerateLane();
    }
}