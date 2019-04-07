using System;
using UnityEngine;

public class KillTool : MonoBehaviour
{
    public event Action ZombieKilled;
    public bool CanHurt { get; set; }
    private Rigidbody _rigidbody;
    private Vector3 _lastPosition;
    private Vector3 _movement;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movement = _rigidbody.position;
    }

    public void FixedUpdate()
    {
        _movement = (_rigidbody.position - _lastPosition).normalized;
        _lastPosition = _rigidbody.position;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!CanHurt)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            var death = collision.gameObject.GetComponentInParent<Death>();
            death.Kill((_movement + Vector3.up)*100);
            ZombieKilled?.Invoke();
            //var joint = collision.gameObject.AddComponent<FixedJoint>();
            //joint.connectedBody = _rigidbody;
            //var direction = collision.contacts[0].normal*500;
            //collision.rigidbody.AddForce(-direction, ForceMode.Impulse);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Buildable"))
        {
            collision.gameObject.GetComponent<BuiltObject>().Destroy();
        }
    }
}
