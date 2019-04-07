using Assets.Code.Attractions;
using UnityEngine;

public class SwingLogic2 : AttractionLogic
{
    public ConfigurableJoint Arm;
    private Rigidbody _rigidbody;
    private int _direction;
    private float _pushStart;

    public void Start()
    {
        _rigidbody = Arm.gameObject.GetComponent<Rigidbody>();
        _pushStart = Time.time + 3;
    }

    public void FixedUpdate()
    {
        Debug.DrawLine(Arm.transform.position, Arm.transform.position + Arm.transform.right * 10, Color.blue, 10);
        Debug.Log(_rigidbody.angularVelocity);
        _rigidbody.AddRelativeTorque(Arm.transform.forward * 100000);
        //if (_rigidbody.angularVelocity.z < 0 || Time.time < _pushStart)
        //{
        //    _direction = -_direction;
        //    if(!Arm.useMotor)
        //    {
        //        Arm.useMotor = true;
        //        Debug.Log("TURNING ON");
        //    }
        //}
        //else
        //{
        //    if(Arm.useMotor)
        //    {
        //        Arm.useMotor = false;
        //        Debug.Log("TURNING OFF");
        //    }
        //}
    }
}