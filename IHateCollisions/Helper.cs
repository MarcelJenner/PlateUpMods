global using static DoorsAreAnnoying.Helper;
global using UnityEngine;
global using Kitchen;
global using KitchenMods;

namespace DoorsAreAnnoying
{
    public static class Helper
    {
        public static void Log(string message)
        {
            Debug.Log($"[{nameof(IHateCollisions)}]: {message}");
        }
    }
}