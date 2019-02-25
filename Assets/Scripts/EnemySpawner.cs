using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfiguration> waveConfigurations;

    WaveConfiguration currentWave;
    int currentWaveIndex = 0;
    Coroutine currentWaveCoroutine;

    void Start() {
        StartNextWave();
    }

    private void StartNextWave() {
        if (currentWaveCoroutine != null) {
            StopCoroutine(currentWaveCoroutine);
        }

        currentWave = waveConfigurations[currentWaveIndex];

        currentWaveCoroutine = StartCoroutine(MakeWaveCoroutine());
    }

    private IEnumerator MakeWaveCoroutine() {
        for (int enemyCount = 0; enemyCount < currentWave.GetNumberOfEnemiesToSpawn(); enemyCount++) {
            SpawnEnemy();
            
            yield return new WaitForSeconds(currentWave.GetTimeBetweenWaves() + Random.Range(currentWave.GetRandomFactor() * -1, currentWave.GetRandomFactor()));
        }

        currentWaveIndex++;

        if (currentWaveIndex >= waveConfigurations.Count) {
            currentWaveIndex = 0;
        }

        StartNextWave();
    }

    private void SpawnEnemy() {
        List<Transform> waypoints = currentWave.GetWaypoints();

        GameObject enemy = Instantiate(currentWave.GetEnemyPrefab(), waypoints[1].position, Quaternion.identity);
        EnemyPathing enemyController = enemy.GetComponent<EnemyPathing>();
        enemyController.SetWaypoints(waypoints);
        enemyController.SetMovementSpeed(currentWave.GetMovementSpeed());
    }
}
