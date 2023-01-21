using System.Reflection;

namespace IHateCollisions.Systems;

public class HarmonyPatchSystem : GenericSystemBase, IModSystem
{
    public static readonly string VERSION = "0.1";
    protected override void Initialise()
    {
        if (Object.FindObjectOfType<IHateCollisionsPatcher>() != null)
            return;
        var prepGhostMod = new GameObject("IHateCollisions");
        prepGhostMod.AddComponent<IHateCollisionsPatcher>();
        Object.DontDestroyOnLoad(prepGhostMod);
    }

    protected override void OnUpdate() {}
}

public class IHateCollisionsPatcher : MonoBehaviour
{
    private readonly HarmonyLib.Harmony harmony = new HarmonyLib.Harmony("happening.plateup.plateupprepghost");

    private void Awake()
    {
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}