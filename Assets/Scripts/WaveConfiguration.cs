using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfiguration", menuName = "Configuration/Wave", order = 1)]
public class WaveConfiguration : ScriptableObject
{
    public string title;
    public GameObject enemyPrefab;
    public GameObject pathPrefab;
    public float timeBetweenEnemySpawns = 1f;
    public float randomFactor = 0.3f;
    public int numberOfEnemiesToSpawn = 10;
    public float movementSpeed = 2f;
    
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

    public float GetTimeBetweenSpawns() {
        return timeBetweenEnemySpawns;
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
