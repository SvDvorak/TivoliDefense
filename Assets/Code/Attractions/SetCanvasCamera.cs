using UnityEngine;

public class SetCanvasCamera : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
