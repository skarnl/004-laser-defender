using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfiguration", menuName = "Configuration/Wave", order = 1)]
public class WaveConfiguration : ScriptableObject
{
    [SerializeField] string title;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] float randomFactor = 0.3f;
    [SerializeField] int numberOfEnemiesToSpawn = 10;
    [SerializeField] float movementSpeed = 2f;
    
    public GameObject GetEnemyPrefab() {
        return enemyPrefab;
    }

    public List<Transform> GetWaypoints() {
        var waveWaypoints = new List<Transform>();

        foreach( Transform waypoint in pathPrefab.GetComponentsInChildren<Transform>()) {
            waveWaypoints.Add(waypoint);
        }

        return waveWaypoints;
    }

    public float GetTimeBetweenWaves() {
        return timeBetweenWaves;
    }

    public float GetRandomFactor() {
        return randomFactor;
    }

    public int GetNumberOfEnemiesToSpawn() {
        return numberOfEnemiesToSpawn;
    }

    public float GetMovementSpeed() {
        return movementSpeed;
    }

    public string GetTitle() {
        return title;
    }
}
