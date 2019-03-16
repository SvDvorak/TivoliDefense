using Assets;
using UnityEngine;

public class GameoverArea : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToUpper() == "ZOMBIE")
        {
            Debug.Log("GAME OVER");

            Gamestate.Gameover = true;
        }
    }
}