using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorShoot : MonoBehaviour
{

    public SphereCollider ZombieAttackTrigger;
    public List<Death> NearbyZombies = new List<Death>();
    private Death ZombieTarget = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootZombies(Random.Range(3.5f, 4.5f)));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            var death = other.gameObject.GetComponent<Death>();
            death.onKilled += OnKilled;
            NearbyZombies.Add(death);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            var death = other.gameObject.GetComponent<Death>();
            death.onKilled -= OnKilled;
            NearbyZombies.Remove(death);
        }
    }

    private IEnumerator ShootZombies(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            var zombie = FindNearestZombie();
            if (zombie == null) continue;

            Debug.DrawLine(transform.position, zombie.transform.position, Color.blue, time);
            NearbyZombies.Remove(zombie);
            zombie.Kill();
        }
    
    }

    public void OnKilled(Death death)
    {
        NearbyZombies.Remove(death);
        death.onKilled -= OnKilled;
    }

    private Death FindNearestZombie()
    {
        if (NearbyZombies.Count == 0) return null;

        var nearestZombie = NearbyZombies[0];
        var nearestDistance = Vector3.Distance(transform.position, nearestZombie.transform.position);

        foreach (var zombie in NearbyZombies)
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
