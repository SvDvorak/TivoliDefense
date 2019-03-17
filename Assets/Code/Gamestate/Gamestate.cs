using System.Collections.Generic;
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
        }

        public static void SetWindowState(OpenModification openModification, bool open)
        {
            if (WindowOpen)
                WindowOpen.Close();

            if (open)
            {
                openModification.Open();
                WindowOpen = openModification;
            }
            else
            {
                openModification.Close();
                WindowOpen = null;
            }
        }
    }
}