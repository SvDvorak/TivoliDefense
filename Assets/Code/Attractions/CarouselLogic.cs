using System.Collections.Generic;
using UnityEngine;

public class CarouselLogic : MonoBehaviour, IAttraction
{
    public List<KillTool> KillTools = new List<KillTool>();
    public ParticleSystem Smoke;

    private float _startTime = 0;
    private int _kills;
    private float _spinSpeed;
    private float _maxSpeed = 180;
    private bool _broken;

    public void Start()
    {
        _startTime = Time.time;
        foreach (var killTool in KillTools)
        {
            killTool.ZombieKilled += ZombieKilled;
        }
    }

    private void ZombieKilled()
    {
        _kills += 1;
    }

    public void Update()
    {
        var changedSpeed = _broken ? -Time.deltaTime * 30 : Time.deltaTime * 60;
        _spinSpeed = Mathf.Clamp(_spinSpeed + changedSpeed, 0, _maxSpeed);

        transform.rotation *= Quaternion.Euler(0, _spinSpeed*Time.deltaTime, 0);

        var wear = Time.time - _startTime + _kills * 1;
        if (wear > 20 && !_broken)
        {
            BreakDown();
        }

        var canHurt = _spinSpeed > 30;
        foreach (var killTool in KillTools)
        {
            killTool.CanHurt = canHurt;
        }
    }

    private void BreakDown()
    {
        _broken = true;
        Smoke.Play();
        Debug.Log("BROKEN");
    }

    public void Repair()
    {
        _kills = 0;
        _startTime = Time.time;
        _broken = false;
        Smoke.Stop();
    }
}