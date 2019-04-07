using System.Linq;
using Assets;
using UnityEngine;

public class BuiltTracker : MonoBehaviour
{
    public void Start()
    {
        Gamestate.Breakables = GetComponentsInChildren<Breakable>().ToList();
    }
}
