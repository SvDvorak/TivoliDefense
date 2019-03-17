using Assets;
using UnityEngine;

public class GameoverArea : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            Debug.Log("GAME OVER");

            Gamestate.Gameover = true;
        }
    }
}