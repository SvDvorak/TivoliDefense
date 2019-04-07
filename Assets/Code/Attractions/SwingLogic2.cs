using Assets.Code.Attractions;
using UnityEngine;

public class SwingLogic2 : AttractionLogic
{
    public ConfigurableJoint Arm;
    public Transform ForcePosition;
    private Rigidbody _rigidbody;
    private int _direction;
    private float _pushStart;

    public void Start()
    {
        _rigidbody = Arm.gameObject.GetComponent<Rigidbody>();
        _pushStart = Time.time + 3;
        foreach (var killTool in KillTools)
        {
            killTool.CanHurt = true;
        }
    }

    public override void Update()
    {
        
    }

    public void FixedUpdate()
    {
        Debug.Log(_rigidbody.angularVelocity);
        if(ForcePosition != null)
        {
            var forceDir = Arm.transform.forward;
            var force = forceDir * 1000;
            Debug.DrawLine(ForcePosition.position, ForcePosition.position + forceDir * 3, Color.blue, 10);
            //_rigidbody.AddForceAtPosition(force, ForcePosition.position, ForceMode.Force);
            //_rigidbody.AddTorque(Arm.transform.right * 1000);
        }
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