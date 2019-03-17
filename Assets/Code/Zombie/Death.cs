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
        Kill();
    }

    public void Update()
    {
        
    }

    [ContextMenu("KILL")]
    public void Kill()
    {
        onKilled?.Invoke(this);
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<MoveToTarget>().enabled = false;
        //rb.AddForce(Random.insideUnitSphere*10, ForceMode.Impulse);

        RemoveLayerOnAllChildren(transform);
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
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
