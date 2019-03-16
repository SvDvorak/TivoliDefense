using UnityEngine;
using UnityEngine.AI;

public class ActivateCollidable : MonoBehaviour
{
    public void Activate()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<NavMeshObstacle>().enabled = true;
    }
}
