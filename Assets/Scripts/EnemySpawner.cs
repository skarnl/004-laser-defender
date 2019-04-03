using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class ActiveWave {
    public WaveConfiguration waveConfiguration;
    public int enemiesToSpawn;
    public float lastSpawnTime;

    public ActiveWave(WaveConfiguration waveConfiguration) {
        this.waveConfiguration = waveConfiguration;
        this.enemiesToSpawn = waveConfiguration.GetNumberOfEnemiesToSpawn();
        this.lastSpawnTime = 0;
    }

    public override string ToString() {
        return this.waveConfiguration.GetTitle();
    }
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<GameObject> pathPrefabs;
    List<WaveConfiguration> waveConfigurations;
    [SerializeField] int wavesActiveSimultaneously = 2;
    [SerializeField] bool looping = true;

    List<ActiveWave> activeWaves = new List<ActiveWave>();

    IEnumerator Start() {
        // fill the activeWave List with the needed waves
        if (activeWaves.Count < wavesActiveSimultaneously) {
            for (int i = activeWaves.Count; i < wavesActiveSimultaneously; i++) {
                activeWaves.Add( MakeActiveWave(GetRandomWave()) ) ;
            }
        }

        activeWaves.Sort( delegate(ActiveWave x, ActiveWave y) {
            var nextSpawnTimeX = x.lastSpawnTime + x.waveConfiguration.GetTimeBetweenSpawns();
            var nextSpawnTimeY = y.lastSpawnTime + y.waveConfiguration.GetTimeBetweenSpawns();

            if (nextSpawnTimeX < nextSpawnTimeY) return -1;
            if (nextSpawnTimeX > nextSpawnTimeY) return 1;
            
            return 0;
        });

        PrintDebug();

        do {
            yield return StartCoroutine(SpawnActiveWave());
        } while (looping);
    }

    private WaveConfiguration GetRandomWave() {
        var waveConfig = new WaveConfiguration();

        var random = new System.Random();

        waveConfig.title = "wave config";
        waveConfig.enemyPrefab = enemyPrefabs[random.Next(enemyPrefabs.Count)];
        waveConfig.pathPrefab = pathPrefabs[random.Next(pathPrefabs.Count)];
        waveConfig.timeBetweenEnemySpawns = UnityEngine.Random.Range(0.4f, 1.6f);
        waveConfig.randomFactor = UnityEngine.Random.Range(0.05f, 0.2f);
        waveConfig.movementSpeed = UnityEngine.Random.Range(4f, 7f);
        waveConfig.numberOfEnemiesToSpawn = UnityEngine.Random.Range(3, 10);

        return waveConfig;
    }

    private ActiveWave MakeActiveWave(WaveConfiguration waveConfiguration) {
        return new ActiveWave(waveConfiguration);
    }

    private IEnumerator SpawnActiveWave() {
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

            T (secondes)
            1 spawn waveConfig1 (X) - nu moeten we wachten tot 10 - 0 = 10 tot waveConfig1, maar 3 tot waveConfig2, dus die komt eerder
            2 
            3 
            4 spawn waveConfig2 - nu moeten we nog 10 - (T-X) = 7 wachten tot waveConfig1, maar 3 tot waveConfig2, dus die komt eerder
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
        var currentActiveWave = activeWaves.First();
        
        print("CURRENT:                                         " + currentActiveWave);

        SpawnEnemy(currentActiveWave.waveConfiguration);

        currentActiveWave.enemiesToSpawn -= 1;
        currentActiveWave.lastSpawnTime = Time.time;

        print("ENEMIES LEFT: " + currentActiveWave.enemiesToSpawn);

        if (currentActiveWave.enemiesToSpawn == 0) {
            print("REMOVE:                   " + currentActiveWave);
            activeWaves.RemoveAt(0);
        }

        //DRYYYYY !!!
        activeWaves.Sort( delegate(ActiveWave x, ActiveWave y) {
            var nextSpawnTimeX = x.lastSpawnTime + x.waveConfiguration.GetTimeBetweenSpawns();
            var nextSpawnTimeY = y.lastSpawnTime + y.waveConfiguration.GetTimeBetweenSpawns();

            if (nextSpawnTimeX < nextSpawnTimeY) return -1;
            if (nextSpawnTimeX > nextSpawnTimeY) return 1;
            
            return 0;
        });

        var nextActiveWave = activeWaves.First();

        yield return new WaitForSeconds(nextActiveWave.waveConfiguration.GetTimeBetweenSpawns());

        if (activeWaves.Count < wavesActiveSimultaneously) {
            for (int i = activeWaves.Count; i < wavesActiveSimultaneously; i++) {
                var wave = MakeActiveWave(GetRandomWave());

                activeWaves.Add( wave ) ;

                print("ADD:                 " + wave);
            }
        }
    }

    private void SpawnEnemy(WaveConfiguration waveConfiguration) {
        List<Transform> waypoints = waveConfiguration.GetWaypoints();

        GameObject enemy = Instantiate(waveConfiguration.GetEnemyPrefab(), waypoints[1].position, Quaternion.identity);
        EnemyPathing enemyController = enemy.GetComponent<EnemyPathing>();
        enemyController.SetWaypoints(waypoints);
        enemyController.SetMovementSpeed(waveConfiguration.GetMovementSpeed());
    }

    private void PrintDebug() {
        print("========  ActiveWave configuratie ========= ");
        foreach (var item in activeWaves)
        {
            print(item.waveConfiguration.GetTitle() + " | " + item.lastSpawnTime + " | " + item.waveConfiguration.GetTimeBetweenSpawns() + " | " + (item.lastSpawnTime + item.waveConfiguration.GetTimeBetweenSpawns()) );
        }
        print("============================================");
    }
}
