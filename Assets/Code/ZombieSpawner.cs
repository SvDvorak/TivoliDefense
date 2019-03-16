using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : Singleton<ZombieSpawner>
{
    public float WaveInterval;

    public int ZombiesPerWave;
    public float SpawnPointDelay;
    public int MinBatch;
    public int MaxBatch;
    public float MinBatchDelay;
    public float MaxBatchDelay;

    public GameObject ZombieContainer;
    public SpawnPoint[] SpawnPoints;
    public ZombieTypes ZombieTypes;

    void Start()
    {

        StartCoroutine(DoSpawn());
    }

    void Update()
    {

    }

    private IEnumerator DoSpawn()
    {
        while (true)
        {
            var randomIndex = Random.Range(0, SpawnPoints.Length);

            TriggerSpawnPoint(SpawnPoints[randomIndex]);

            yield return new WaitForSeconds(WaveInterval);
        }
    }

    private void TriggerSpawnPoint(SpawnPoint spawnPoint)
    {
        spawnPoint.SpawnZombies(ZombiesPerWave, SpawnPointDelay, MinBatchDelay, MaxBatchDelay, MinBatch, MaxBatch);
    }

}

[System.Serializable]
public struct ZombieTypes
{
    public GameObject StandardZombie;
}
