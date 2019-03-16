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
    }

    private IEnumerator WaitAndKill()
    {
        yield return new WaitForSeconds(Random.value * 20);
        Kill();
    }

    public void Update()
    {
        
    }

    public void Kill()
    {
        onKilled?.Invoke(this);
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<MoveToTarget>().enabled = false;
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(Random.insideUnitSphere*10, ForceMode.Impulse);
        gameObject.layer = 0;
    }
}
