using System;
using UnityEngine;
using UnityEngine.AI;

public class BuiltObject : MonoBehaviour
{
    public event Action Destroyed;

    public void Activate()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<NavMeshObstacle>().enabled = true;
    }

    public void Destroy()
    {
        Destroyed?.Invoke();
        Destroy(gameObject);
    }
}
