using System;
using UnityEngine;

public class KillTool : MonoBehaviour
{
    public event Action ZombieKilled;
    public bool CanHurt { get; set; }

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!CanHurt)
            return;

        switch (collision.gameObject.tag.ToUpper())
        {
            case "ZOMBIE":
                collision.gameObject.GetComponent<Death>().Kill();
                ZombieKilled?.Invoke();
                break;
            case "BUILDABLE":
                collision.gameObject.GetComponent<BuiltObject>().Destroy();
                break;
        }
    }
}
