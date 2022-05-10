using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<BeeWaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Looping of multiple bee waves
        do
        {
            yield return StartCoroutine(SpawnAllBeeWaves());


        }
        while (looping);

        
    }

    private IEnumerator SpawnAllBeeWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllBeesInWave(currentWave));

        }
    }

    private IEnumerator SpawnAllBeesInWave(BeeWaveConfig waveConfig)
    {
        for (int pinkBeeCount = 0; pinkBeeCount < waveConfig.GetNumOfEnemies(); pinkBeeCount++)
        {
            Instantiate(waveConfig.GetPinkBeePrefab(),
            waveConfig.GetWaypoints()[0].transform.position,
               Quaternion.identity);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());

        }

    }


}
