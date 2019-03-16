using Assets;
using UnityEngine;

public class ShowOnGameover : MonoBehaviour
{
    public GameObject ToShow;

    public void Start()
    {
    }

    public void Update()
    {
        if(Gamestate.Gameover && !ToShow.activeSelf)
            ToShow.SetActive(true);
    }
}
