using System.Reflection;

namespace FasterConveyors.Systems;

public class HarmonyPatchSystem : GenericSystemBase, IModSystem
{
    protected override void Initialise()
    {
        if (Object.FindObjectOfType<IHateCollisionsPatcher>() != null)
            return;
        var prepGhostMod = new GameObject("FasterConveyors");
        prepGhostMod.AddComponent<IHateCollisionsPatcher>();
        Object.DontDestroyOnLoad(prepGhostMod);
    }

    protected override void OnUpdate() {}
}

public class IHateCollisionsPatcher : MonoBehaviour
{
    private readonly HarmonyLib.Harmony harmony = new HarmonyLib.Harmony("happening.plateup.fasterconveyors");

    private void Awake()
    {
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}