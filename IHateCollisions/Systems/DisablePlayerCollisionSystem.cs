namespace IHateCollisions.Systems;

public class DisablePlayerCollisionSystem : GenericSystemBase, IModSystem
{
    private static bool _disablePlayerCollisionOption = true;
    public static bool DisablePlayerCollisionOption
    {
        get => _disablePlayerCollisionOption;
        set
        {
            _settingsChanged = true;
            _disablePlayerCollisionOption = value;
        }
    }
    private static bool _settingsChanged = true;
        
    private const int LayerPlayers = 12;
        
    protected override void OnUpdate() {

        if (_settingsChanged)
        {
            Log($"{(DisablePlayerCollisionOption ? "disabling": "enabling")} player collisions");
            Physics.IgnoreLayerCollision(LayerPlayers, LayerPlayers, DisablePlayerCollisionOption);
            _settingsChanged = false;
        }
    }
}