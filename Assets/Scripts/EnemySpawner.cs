using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfiguration> waveConfigurations;
    [SerializeField] bool looping = true;

    IEnumerator Start() {
        do {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves() {
        for( int waveIndex = 0; waveIndex < waveConfigurations.Count; waveIndex++ ) {
            var currentWave = waveConfigurations[waveIndex];

            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfiguration waveConfiguration) {
        for (int enemyCount = 0; enemyCount < waveConfiguration.GetNumberOfEnemiesToSpawn(); enemyCount++) {
            SpawnEnemy(waveConfiguration);
            
            yield return new WaitForSeconds(waveConfiguration.GetTimeBetweenWaves() + Random.Range(waveConfiguration.GetRandomFactor() * -1, waveConfiguration.GetRandomFactor()));
        }
    }

    private void SpawnEnemy(WaveConfiguration waveConfiguration) {
        List<Transform> waypoints = waveConfiguration.GetWaypoints();

        GameObject enemy = Instantiate(waveConfiguration.GetEnemyPrefab(), waypoints[1].position, Quaternion.identity);
        EnemyPathing enemyController = enemy.GetComponent<EnemyPathing>();
        enemyController.SetWaypoints(waypoints);
        enemyController.SetMovementSpeed(waveConfiguration.GetMovementSpeed());
    }
}
