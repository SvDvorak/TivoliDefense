using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Death : MonoBehaviour
{
    public delegate void OnKilled(Death @object);
    public OnKilled onKilled;

    public void Start()
    {
        //StartCoroutine(WaitAndKill());
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
    }

    private IEnumerator WaitAndKill()
    {
        yield return new WaitForSeconds(Random.value * 20);
        Kill(Vector3.zero);
    }

    public void Update()
    {
        
    }

    [ContextMenu("KILL")]
    public void Kill(Vector3 force)
    {
        onKilled?.Invoke(this);
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<MoveToTarget>().enabled = false;

        RemoveLayerOnAllChildren(transform);
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }

    public void RemoveLayerOnAllChildren(Transform parent)
    {
        parent.gameObject.layer = 0;
        foreach (Transform child in parent)
        {
            RemoveLayerOnAllChildren(child);
        }
    }
}
