using System;
using System.Collections;
using Assets;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MoveToTarget : MonoBehaviour
{
    public Transform TargetPosition;
    public float MaxSpeed = 3;

    private NavMeshAgent _navAgent;
    private Animator _animator;
    private Breakable _breakTarget;

    public void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        //_navAgent.SetDestination(Vector3.zero);
        _navAgent.speed = 1 + Random.value*0.5f;

        //StartCoroutine(TargetPosition != null
        //    ? DelayStart(Random.value, () => _navAgent.SetDestination(TargetPosition.position))
        //    : FollowMouseCursor());

        _animator = GetComponent<Animator>();
    }

    private IEnumerator DelayStart(float time, Action func)
    {
        yield return new WaitForSeconds(time);
        func();
    }

    private IEnumerator FollowMouseCursor()
    {
        yield return new WaitForSeconds(Random.value * 3);
        while (enabled)
        {
            _navAgent.SetDestination(Gamestate.InputGroundPosition);
            yield return new WaitForSeconds(3);
        }
    }

    public void Update()
    {
        if (Gamestate.Gameover)
        {
            _navAgent.isStopped = true;
            enabled = false;
        }

        //if (TargetPosition == null && _breakTarget == null)
        if (_breakTarget == null && Gamestate.HasBreakableLeft)
        {
            _breakTarget = Gamestate.Breakables[Random.Range(0, Gamestate.Breakables.Count - 1)];
            SetNewTarget(_breakTarget.transform);
        }

        if(_breakTarget != null)
        {
            var isNextToBreakable = (_breakTarget.transform.position - transform.position).sqrMagnitude < 1;
            _animator.SetBool("Attacking", isNextToBreakable);
            _breakTarget.GettingHitBy(this, isNextToBreakable);

            if (_breakTarget.IsBroken)
                _breakTarget = null;
        }
        else
        {
            _animator.SetBool("Attacking", false);
        }

        if (_animator != null)
            _animator.SetFloat("Distance", Mathf.Clamp(_navAgent.remainingDistance, 0.2f, MaxSpeed) * 1.3f);
    }

    private void SetNewTarget(Transform target)
    {
        TargetPosition = target;
        _navAgent.SetDestination(target.position);
    }
}
