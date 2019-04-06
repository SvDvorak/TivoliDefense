using UnityEngine;

public class SpinMechanical2 : MonoBehaviour, IMechanical
{
    private Rigidbody _rigidbody;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Activate()
    {
    }

    public void Deactivate()
    {
    }

    public void FixedUpdate()
    {
        Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.red);
        _rigidbody.AddTorque(transform.up * 1000);
        Debug.Log(_rigidbody.angularVelocity);
    }
}