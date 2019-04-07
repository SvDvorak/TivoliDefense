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

            var zombie = FindNearestZombieWithinFov();
            if (zombie == null) continue;

            var directionToZombie = zombie.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(directionToZombie, Vector3.up);

            Debug.DrawLine(transform.position, zombie.transform.position, Color.blue, time);

            var gunTrailInstance = Instantiate(GunTrail, null);
            gunTrailInstance.StartPosition = transform.position;
            gunTrailInstance.EndPosition = zombie.transform.position;

            _audioSource.Play();

            AwarenessSphere.NearbyZombies.Remove(zombie);
            zombie.Kill((zombie.transform.position - transform.position).normalized*30);
        }
    
    }

   

    private Death FindNearestZombieWithinFov()
    {
        if (AwarenessSphere.NearbyZombies.Count == 0) return null;

        var nearestZombie = AwarenessSphere.NearbyZombies[0];
        var nearestDistance = Vector3.Distance(transform.position, nearestZombie.transform.position);

        foreach (var zombie in AwarenessSphere.NearbyZombies)
        {
            var distance = Vector3.Distance(transform.position, nearestZombie.transform.position);
            var fromSurvivorToZombie = zombie.transform.position - transform.position;

           

            Debug.DrawLine(transform.position, transform.position + transform.forward * 5, Color.red, 2f);

            var angle = Vector3.Angle(transform.forward, fromSurvivorToZombie.normalized);
            Debug.Log($"ANgle: {angle}");

            if (angle < 70 && distance < nearestDistance)
            {
                Debug.DrawLine(transform.position, transform.position + fromSurvivorToZombie, Color.blue, 2f);
                nearestZombie = zombie;
                nearestDistance = distance;
            }
        }

        var fromSurvivorToFinalZombie = nearestZombie.transform.position - transform.position;
        var finalAngle = Vector3.Angle(transform.forward, fromSurvivorToFinalZombie.normalized);

        if (finalAngle > 70) return null;

        return nearestZombie;
    }
}
