using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorSphereOfAwareness : MonoBehaviour
{
    public List<Death> NearbyZombies = new List<Death>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            var death = other.gameObject.GetComponentInParent<Death>();
            death.onKilled += OnKilled;
            NearbyZombies.Add(death);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            var death = other.gameObject.GetComponentInParent<Death>();
            death.onKilled -= OnKilled;
            NearbyZombies.Remove(death);
        }
    }

    public void OnKilled(Death death)
    {
        NearbyZombies.Remove(death);
        death.onKilled -= OnKilled;
    }

}
