using UnityEngine;

namespace Assets
{
    public static class Gamestate
    {
        public static Vector3 InputGroundPosition;
        public static bool Gameover { get; set; }
        public static OpenModification WindowOpen { get; set; }

        public static void Reset()
        {
            Gameover = false;
            WindowOpen = null;
        }
    }
}