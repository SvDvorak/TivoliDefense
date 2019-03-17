using UnityEngine;

public interface IMechanical
{
    void Activate();
    void Deactivate();
}

public class SpinMechanical : MonoBehaviour, IMechanical
{
    public float Speed;
    public float Acceleration;

    private bool _active;
    private float _currentSpeed;

    public void Start()
    {
        Activate();
    }

    public void Activate()
    {
        _active = true;
    }

    public void Deactivate()
    {
        _active = false;
    }

    public void Update()
    {
        var changedSpeed = _active ? Time.deltaTime * Acceleration * 2 : -Time.deltaTime * Acceleration;
        _currentSpeed = Mathf.Clamp(_currentSpeed + changedSpeed, 0, 1);

        Speed = 180;
        transform.rotation *= Quaternion.Euler(0, _currentSpeed * Speed * Time.deltaTime, 0);
    }
}
