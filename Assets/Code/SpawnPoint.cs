using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPoint : MonoBehaviour
{
    public float Radius;
    public SpawnIndicator SpawnIndicator;

    private void Start()
    {
        SetIndicatorActive(false);        
    }

    public void SetIndicatorActive(bool active = true)
    {
        SpawnIndicator.gameObject.SetActive(active);
    }

    public void SpawnZombies(int zombiesToSpawn, float initialDelay, float batchDelayMin, float batchDelayMax, int batchMin, int batchMax)
    {
        StartCoroutine(DoSpawnZombies(zombiesToSpawn, initialDelay, batchDelayMin, batchDelayMax, batchMin, batchMax));
    }

    public IEnumerator DoSpawnZombies(int zombiesToSpawn, float initialDelay, float batchDelayMin, float batchDelayMax, int batchMin, int batchMax)
    {

        SetIndicatorActive(true);

        // Wait an initial delay
        yield return new WaitForSeconds(initialDelay);

        while (zombiesToSpawn > 0)
        {
            var min = Mathf.Min(batchMin, zombiesToSpawn);
            var max = Mathf.Min(batchMax, zombiesToSpawn);

            var batchSize = Random.Range(min, max);

            for (int i = 0; i < batchSize; i++)
            {
                var insideUnitCircle = Random.insideUnitCircle;
                var position = new Vector3(insideUnitCircle.x * Radius, 1, insideUnitCircle.y * Radius) + transform.position;
                position.y = 0;

                var zombieInstance = Instantiate<GameObject>(ZombieSpawner.Instance.ZombieTypes.StandardZombie, null);
                var agent = zombieInstance.GetComponent<NavMeshAgent>();

                agent.Warp(position);
            }

            zombiesToSpawn -= batchSize;

            var batchDelay = Random.Range(batchDelayMin, batchDelayMax);

            yield return new WaitForSeconds(batchDelay);
        }

        SetIndicatorActive(false);
    }
}
