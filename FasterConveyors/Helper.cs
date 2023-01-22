global using static FasterConveyors.Helper;
global using UnityEngine;
global using Kitchen;
global using KitchenMods;

namespace FasterConveyors;

public static class Helper
{
    public static void Log(string message)
    {
        Debug.Log($"[{nameof(FasterConveyors)}]: {message}");
    }
}