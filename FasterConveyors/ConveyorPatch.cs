using HarmonyLib;

namespace FasterConveyors;

[HarmonyPatch(typeof(ConveyItemsView), nameof(ConveyItemsView.UpdateData))]
public class ConveyorPatch
{
    [HarmonyPrefix]
    public void ChangeConveyorSpeed(ConveyItemsView __instance, ref ConveyItemsView.ViewData ___Data)
    {
        ___Data.PushAmount = 2f;
    }
}