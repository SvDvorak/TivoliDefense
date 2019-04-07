using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Survivor : MonoBehaviour
{
    public SelectionIndicator SelectionIndicator;
    public NavMeshAgent NavMeshAgent;
    public Animator ModelAnimator;
    public Buildable BuildTarget;
    public SurvivorShoot SurvivorShoot;
    private Coroutine _buildRoutine;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetSelectionIndicatorActive(bool active)
    {
        SelectionIndicator.gameObject.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        ModelAnimator.SetFloat(Animator.StringToHash("Speed"), NavMeshAgent.velocity.magnitude);
    }

    public void Build(Buildable buildable)
    {
        _buildRoutine = StartCoroutine(BuildRoutine(buildable));
    }

    public void StopBuild()
    {
        if (_buildRoutine != null)
            StopCoroutine(_buildRoutine);
    }


    private IEnumerator BuildRoutine(Buildable buildable)
    {
        var timeElapsed = 0f;

        NavMeshAgent.SetDestination(buildable.transform.position);

        var distance = Vector3.Distance(buildable.transform.position, transform.position);

        while (distance > 3)
        {

            distance = Vector3.Distance(buildable.transform.position, transform.position);

            if (distance < 3) break;

            yield return new WaitForSeconds(0.1f);
            timeElapsed += Time.deltaTime;
        }

        NavMeshAgent.SetDestination(transform.position);

        while (buildable.BuildProgress < 1f && distance <= 3f)
        {
            distance = Vector3.Distance(buildable.transform.position, transform.position);
            buildable.BuildProgress += 0.1f;
            yield return new WaitForSeconds(0.2f);
        }

        var thingsToBuild = Physics.OverlapSphere(transform.position, 100, LayerMask.GetMask("Buildable"));


        Buildable nearestBuildable = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var thing in thingsToBuild)
        {
           
            var potentialBuildable = thing.transform.GetComponentInParent<Buildable>();

            if (potentialBuildable != null && potentialBuildable.BuildProgress < 1f)
            {
                if (nearestBuildable == null)
                {
                    nearestBuildable = potentialBuildable;
                    nearestDistance = Vector3.Distance(nearestBuildable.transform.position, transform.position);
                }

                var distonce = Vector3.Distance(potentialBuildable.transform.position, transform.position);

                if (distonce < nearestDistance)
                {
                    nearestBuildable = potentialBuildable;
                    nearestDistance = distonce;
                }
            }
        }

        if (nearestBuildable != null)
        {
            Build(nearestBuildable);
        }
    }
}
