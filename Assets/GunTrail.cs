using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrail : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public ParticleSystem _particleSystem;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        transform.position = (StartPosition + EndPosition) / 2f;
        var shape = _particleSystem.shape;

        var distance = Vector3.Distance(StartPosition, EndPosition);

        shape.scale = new Vector3(distance, 1, 1);

        var directionVector = EndPosition - StartPosition;

        directionVector.y = 0;

        var angle = Vector3.Angle(Vector3.left, directionVector);

        shape.rotation = new Vector3(0, angle, 0);

        _particleSystem.Play();
    }
}
