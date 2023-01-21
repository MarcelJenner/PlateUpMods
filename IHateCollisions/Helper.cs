global using static IHateCollisions.Helper;
global using UnityEngine;
global using Kitchen;
global using KitchenMods;

namespace IHateCollisions;

public static class Helper
{
    public static void Log(string message)
    {
        Debug.Log($"[{nameof(IHateCollisions)}]: {message}");
    }
}