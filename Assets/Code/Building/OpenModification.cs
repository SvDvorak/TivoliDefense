using Assets;
using UnityEngine;

public class OpenModification : MonoBehaviour
{
    public GameObject Window;
    
    public void Start()
    {
        
    }

    public void Update()
    {
        Window.transform.rotation = Quaternion.LookRotation(Window.transform.position - Camera.main.transform.position, Vector3.up);
    }

    public void OnMouseDown()
    {
        Window.SetActive(!Window.activeSelf);
        if(Window.activeSelf)
            Gamestate.WindowOpen = this;
    }
}