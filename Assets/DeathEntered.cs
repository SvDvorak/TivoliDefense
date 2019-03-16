using UnityEngine;

public class DeathEntered : MonoBehaviour
{
    public void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Zombie":
                collision.gameObject.GetComponent<Death>().Kill();
                break;
            case "Buildable":
                collision.gameObject.GetComponent<BuiltObject>().Destroy();
                break;
        }
    }
}
