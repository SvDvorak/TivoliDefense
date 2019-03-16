using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Death : MonoBehaviour
{
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
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<MoveToTarget>().enabled = false;
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(Random.insideUnitSphere*10, ForceMode.Impulse);
    }
}
