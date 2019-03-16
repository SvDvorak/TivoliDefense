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
        Gamestate.SetWindowState(this, !Window.activeSelf);
    }

    public void Open()
    {
        Window.SetActive(true);
    }

    public void Close()
    {
        Window.SetActive(false);
    }
}