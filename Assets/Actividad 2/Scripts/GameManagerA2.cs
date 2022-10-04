using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManagerA2 : MonoBehaviour
{
    [Header("Hazard Prefab")]
    public Camera mainCamera;
    public Transform startPoint; //Point from where ground tiles will start
    public Obstacle tilePrefab;
    
    [Tooltip("How many tiles should we create in advance")]
    [Range(1, 15)]
    public int tilesToPreSpawn = 10; //How many tiles should be pre-spawned
    public int tilesWithoutObstacles = 3; //How many tiles at the beginning should not have obstacles, good for warm-up

    List<Obstacle> spawnedTiles = new List<Obstacle>();

    public static GameManagerA2 instance;
    
    [Header("World Prefab")]
    
    [Tooltip("A reference to the tile we want to spawn")]
    public Transform tile;

    [FormerlySerializedAs("startPoint")] [Tooltip("Where the first tile should be placed at")]
    public Vector3 startPointT = new Vector3(0, 0, -5);

    [Tooltip("How many tiles should we create in advance")]
    [Range(1, 15)]
    public int initSpawnNum = 10;

    /// <summary> 
    /// Where the next tile should be spawned at. 
    /// </summary> 
    private Vector3 nextTileLocation;
    /// <summary> 
    /// How should the next tile be rotated? 
    /// </summary> 
    private Quaternion nextTileRotation;
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    
    private void Start()
    {
        
        instance = this;

        Vector3 spawnPosition = startPoint.position;
        int tilesWithNoObstaclesTmp = tilesWithoutObstacles;
        for (int i = 0; i < tilesToPreSpawn; i++)
        {
            spawnPosition -= tilePrefab.startPoint.localPosition;
            Obstacle spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity) as Obstacle;
            if(tilesWithNoObstaclesTmp > 0)
            {
                spawnedTile.DeactivateAllObstacles();
                tilesWithNoObstaclesTmp--;
            }
            else
            {
                spawnedTile.ActivateRandomObstacle();
            }
            
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            spawnedTiles.Add(spawnedTile);
        }
        
        
        // Set our starting point 
	    // Manage Rotation and Orientation
        nextTileLocation = startPointT;
        nextTileRotation = Quaternion.identity;
        for (int i = 0; i < initSpawnNum; ++i)
        {
            SpawnNextTile();
        }
    }
    
    void Update()
    {
        if (mainCamera.WorldToViewportPoint(spawnedTiles[0].endPoint.position).z < 0)
        {
            //Move the tile to the front if it's behind the Camera
            Obstacle tileTmp = spawnedTiles[0];
            spawnedTiles.RemoveAt(0);
            tileTmp.transform.position = spawnedTiles[spawnedTiles.Count - 1].endPoint.position - tileTmp.startPoint.localPosition;
            tileTmp.ActivateRandomObstacle();
            spawnedTiles.Add(tileTmp);
        }
    }

    /// <summary> 
    /// Will spawn a tile at a certain location and setup the next
    /// position 
    /// </summary> 
    public void SpawnNextTile()
    {
        var newTile = Instantiate(tile, nextTileLocation,
        nextTileRotation);
        
        // Figure out where and at what rotation we should spawn
        // the next item 
        var nextTile = newTile.Find("Next Spawn Point");
        nextTileLocation = nextTile.position;
        nextTileRotation = nextTile.rotation;
    }
}
