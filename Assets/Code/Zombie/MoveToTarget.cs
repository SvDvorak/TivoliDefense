﻿using System.Collections;
using Assets;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    public void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        //_navMeshAgent.SetDestination(Vector3.zero);
        _navMeshAgent.speed = 2 + Random.value*2.5f;

        StartCoroutine(FollowMouseCursor());
    }

    private IEnumerator FollowMouseCursor()
    {
        yield return new WaitForSeconds(Random.value * 3);
        while (enabled)
        {
            _navMeshAgent.SetDestination(Gamestate.InputGroundPosition);
            yield return new WaitForSeconds(3);
        }
    }

    public void Update()
    {
        if (Gamestate.Gameover)
        {
            _navMeshAgent.isStopped = true;
            enabled = false;
        }
    }
}