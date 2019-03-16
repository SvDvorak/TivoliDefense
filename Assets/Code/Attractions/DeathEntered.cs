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
        switch (collision.gameObject.tag.ToUpper())
        {
            case "ZOMBIE":
                collision.gameObject.GetComponent<Death>().Kill();
                break;
            case "BUILDABLE":
                collision.gameObject.GetComponent<BuiltObject>().Destroy();
                break;
        }
    }
}
