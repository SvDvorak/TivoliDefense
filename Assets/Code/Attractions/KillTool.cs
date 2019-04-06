using System;
using UnityEngine;

public class KillTool : MonoBehaviour
{
    public event Action ZombieKilled;
    public bool CanHurt { get; set; }

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!CanHurt)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            var death = collision.gameObject.GetComponentInParent<Death>();
            death.Kill(Vector3.zero);
            ZombieKilled?.Invoke();
            //var direction = collision.contacts[0].normal*500;
            //collision.rigidbody.AddForce(-direction, ForceMode.Impulse);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Buildable"))
        {
            collision.gameObject.GetComponent<BuiltObject>().Destroy();
        }
    }
}
