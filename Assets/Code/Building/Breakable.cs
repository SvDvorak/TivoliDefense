using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityEngine.AI;

public class Breakable : MonoBehaviour
{
    public float Health;
    public float DamagePerAttack;

    private readonly List<MoveToTarget> _peopleBreaking = new List<MoveToTarget>();
    public bool IsBroken;
    private SkinnedMeshRenderer _meshRenderer;

    public void Start()
    {
        _meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    public void Update()
    {
        Health -= DamagePerAttack * _peopleBreaking.Count * Time.deltaTime;

        if (Health < 0 && !IsBroken)
        {
            StartCoroutine(AnimateBreakage());
            IsBroken = true;
        }
    }

    public IEnumerator AnimateBreakage()
    {
        const float animationTime = 0.5f;
        var endTime = Time.time + animationTime;
        while(endTime > Time.time)
        {
            var ratioRemaining = (endTime - Time.time) / animationTime;
            _meshRenderer.SetBlendShapeWeight(0, ratioRemaining * 100);
            yield return null;
        }
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshObstacle>().enabled = false;
        _peopleBreaking.Clear();
        Gamestate.Breakables.Remove(this);
    }

    public void GettingHitBy(MoveToTarget person, bool isNextToBreakable)
    {
        if(isNextToBreakable && !_peopleBreaking.Contains(person))
            _peopleBreaking.Add(person);
        else if (!isNextToBreakable && _peopleBreaking.Contains(person))
            _peopleBreaking.Remove(person);
    }
}
