using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfiguration> waveConfigurations;
    [SerializeField] bool looping = true;
    [SerializeField] int wavesActiveSimultaneously = 2;

    IEnumerator Start() {
        do {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves() {
        var shuffledWaveConfiguration = new List<WaveConfiguration>(waveConfigurations);

        // shuffle the order of wave configs
        for (int i = 0; i < shuffledWaveConfiguration.Count; i++) {
            WaveConfiguration temp = shuffledWaveConfiguration[i];
            int randomIndex = Random.Range(i, shuffledWaveConfiguration.Count);
            shuffledWaveConfiguration[i] = shuffledWaveConfiguration[randomIndex];
            shuffledWaveConfiguration[randomIndex] = temp;
        }

        var rest = shuffledWaveConfiguration.Count % wavesActiveSimultaneously;

        // add some more if the length of the list isn't devidable by the wavesActiveSimultaneously
        if (rest != 0) {
            for (int i = 0; i < (wavesActiveSimultaneously - rest); i++) {   
                shuffledWaveConfiguration.Add(shuffledWaveConfiguration[i]);
            }
        }

        // var subList = shuffledWaveConfiguration.GetRange(0, wavesActiveSimultaneously);



        /*
            pak wavesActiveSimultaneously (bijvoorbeeld 2) keer de waves eruit
            loop door de waves, pak steeds uit de waveConfig die aan de beurt is een enemy en spawn die
            en bereken dan hoeveel tijd we moeten wachten tot we de volgende waveConfig pakken

            bijvoorbeeld:

            waveConfig1 = 5 enemies, 10 sec tussen de enemies
            waveConfig2 = 3 enemies, 3 sec tussen de enemies
            waveConfig3 = 4 enemies, 6 sec tussen de enemies

            lijst bijhouden per waveConfig wanneer de laatste keer een spawn is gedaan
            en bij elke spawn, berekenen we dus per config hoelang ze nog moeten wachten
            zodat we dan kunnen bepalen wie de volgende wordt in het rijtje en hoeveel seconden ze moeten wachten

            wat doen we als ze tegelijk zijn? beiden spawnen? zou wel gaafste zijn
            dus eigenlijk gewoon kijken wie er - als de tijd voorbij is - op 0 staat... 

            secondes:
            1 spawn waveConfig1 - nu moeten we wachten tot 10 - 0 = 10 tot waveConfig1, maar 3 tot waveConfig2, dus die komt eerder
            2 
            3 
            4 spawn waveConfig2 - nu moeten we nog 10 - 3 = 7 wachten tot waveConfig1, maar 3 tot waveConfig2, dus die komt eerder
            5 
            6 
            7 spawn waveConfig2 - nu moeten we nog 10 - 6 = 4 wachten tot waveConfig1, maar 3 tot waveConfig2, dus die komt eerder 
            8
            9
            10 spawn waveConfig2 - nu moeten we nog 10 - 9 = 1 wachten tot waveConfig1, en waveConfig2 is op ... dus waveConfig3 gaan we nu
                gebruiken... en die doen we na 6 sec - dus nu komt waveConfig1, over 1 sec
            11 spawn waveConfig1 - nu moeten we 10 - 0 = 10 wachten voor waveConfig1 en nog maar 5 voor waveConfig3
            12
            13
            14
            15
            16 spawn waveConfig3 - nu moeten we nog 10 - 5 = 5 wachten voor waveConfig1 en nog 6 voor waveConfig3
            17 
            18
            19
            20
         */



        for( int waveIndex = 0; waveIndex < shuffledWaveConfiguration.Count; waveIndex += 2 ) {
            
            // yield return null;
            yield return StartCoroutine(SpawnAllEnemiesInWave(shuffledWaveConfiguration[waveIndex]));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfiguration waveConfiguration) {
        yield return null;
        
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
