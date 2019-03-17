using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorShoot : MonoBehaviour
{
    public GunTrail GunTrail;
    public SurvivorSphereOfAwareness AwarenessSphere;
    private AudioSource _audioSource;

    private Death ZombieTarget = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootZombies(Random.Range(3.5f, 4.5f)));
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

   
    private IEnumerator ShootZombies(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            var zombie = FindNearestZombie();
            if (zombie == null) continue;

            Debug.DrawLine(transform.position, zombie.transform.position, Color.blue, time);

            var gunTrailInstance = Instantiate(GunTrail, null);
            gunTrailInstance.StartPosition = transform.position;
            gunTrailInstance.EndPosition = zombie.transform.position;

            _audioSource.Play();

            AwarenessSphere.NearbyZombies.Remove(zombie);
            zombie.Kill();
        }
    
    }

   

    private Death FindNearestZombie()
    {
        if (AwarenessSphere.NearbyZombies.Count == 0) return null;

        var nearestZombie = AwarenessSphere.NearbyZombies[0];
        var nearestDistance = Vector3.Distance(transform.position, nearestZombie.transform.position);

        foreach (var zombie in AwarenessSphere.NearbyZombies)
        {
            var distance = Vector3.Distance(transform.position, nearestZombie.transform.position);

            if (distance < nearestDistance)
            {
                nearestZombie = zombie;
                nearestDistance = distance;
            }
        }

        return nearestZombie;
    }
}
