using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using IHateCollisions.Systems;
using Kitchen.Modules;

namespace IHateCollisions;

[HarmonyPatch(typeof(MainMenu), nameof(MainMenu.Setup))]
class MenuPatch
{
    // INFO: parametername has to be "__instance" because thats the name of the internal plateUp variable thats used as the parameter after the harmony patch
    [HarmonyPrefix]
    public static void Setup_AddPrepGhostMenu(MainMenu __instance) 
    {
        var addSubMenuButtonMethod = GetMethod(__instance.GetType(), "AddSubmenuButton");
        addSubMenuButtonMethod.Invoke(__instance, new object[3] { "IHateCollisions", typeof(IHateCollisionsOptionsMenu), false });
    }

    public static MethodInfo GetMethod(Type instanceType, string name, Type genericType = null)
    {
        var method = instanceType.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
        if (genericType != null)
        {
            method = method.MakeGenericMethod(genericType);
        }
        return method;
    }
}

[HarmonyPatch(typeof(PlayerPauseView), "SetupMenus")]
class PausePatch
{
    [HarmonyPrefix]
    public static void SetupMenus_AddPrepGhostMenu(PlayerPauseView __instance)
    {
        var moduleList = __instance.GetType().GetField("ModuleList", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(__instance) as ModuleList;
        var mInfo = MenuPatch.GetMethod(__instance.GetType(), "AddMenu");

        mInfo.Invoke(__instance, new object[2] { typeof(IHateCollisionsOptionsMenu), new IHateCollisionsOptionsMenu(__instance.ButtonContainer, moduleList) });
    }
}

class IHateCollisionsOptionsMenu : Menu<PauseMenuAction>
{
    public Option<bool> RemoveDoorsOption;

    public Option<bool> DisablePlayerCollisionOption;

    public IHateCollisionsOptionsMenu(Transform container, ModuleList moduleList) : base(container, moduleList) {}

    public override void Setup(int playerId)
    {
        RemoveDoorsOption = GetRemoveDoorsOption();
        AddLabel("Remove doors");
        Add(RemoveDoorsOption);

        DisablePlayerCollisionOption = GetDisablePlayerCollisionOption();
        AddLabel("Remove player collision");
        Add(DisablePlayerCollisionOption);

        AddButton("Back", (Action<int>)(i => RequestPreviousMenu()));
    }

    private Option<bool> GetRemoveDoorsOption()
    {
        var enableOptions = new List<bool>
        {
            false, true
        };
        var current = RemoveDoorsSystem.RemoveDoorsOption;
        var localizationOptions = new List<string>
        {
            "disabled",
            "enabled"
        };

        var removeDoorsOption = new Option<bool>(enableOptions, current, localizationOptions, null);
        removeDoorsOption.OnChanged += delegate (object _, bool value)
        {
            Log($"setting changed - removeDoors : {value}");
            RemoveDoorsSystem.RemoveDoorsOption = value;
        };

        return removeDoorsOption;
    }

    private Option<bool> GetDisablePlayerCollisionOption()
    {
        var enableOptions = new List<bool>
        {
            false, true
        };
        var current = DisablePlayerCollisionSystem.DisablePlayerCollisionOption;
        var localizationOptions = new List<string>
        {
            "disabled",
            "enabled"
        };

        var disablePlayerCollisionOption = new Option<bool>(enableOptions, current, localizationOptions, null);
        disablePlayerCollisionOption.OnChanged += delegate (object _, bool value)
        {
            Log($"setting changed - disablePlayerCollision : {value}");
            DisablePlayerCollisionSystem.DisablePlayerCollisionOption = value;
        };

        return disablePlayerCollisionOption;
    }
}