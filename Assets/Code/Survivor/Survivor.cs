using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Survivor : MonoBehaviour
{
    public SelectionIndicator SelectionIndicator;
    public NavMeshAgent NavMeshAgent;
    public Animator ModelAnimator;

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
}
