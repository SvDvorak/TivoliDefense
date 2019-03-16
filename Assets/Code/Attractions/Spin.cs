using UnityEngine;

public class Spin : MonoBehaviour
{
    public void Start()
    {
        
    }

    public void Update()
    {
        transform.rotation *= Quaternion.Euler(0, Time.deltaTime*180, 0);
    }
}
