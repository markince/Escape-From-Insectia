using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Pink bee Wave Config")]
public class BeeWaveConfig : ScriptableObject
{
    [SerializeField] GameObject pinkBeePrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float pinkBeeMoveSpeed = 2.0f;

    public GameObject GetPinkBeePrefab()
    {
        return pinkBeePrefab;
    }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float GetSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }

    public int GetNumOfEnemies()
    {
        return numberOfEnemies;
    }

    public float GetPinkBeeMoveSpeed()
    {
        return pinkBeeMoveSpeed;
    }


}
