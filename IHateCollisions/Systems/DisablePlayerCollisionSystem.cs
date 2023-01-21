using System.Linq;

namespace DoorsAreAnnoying.Systems
{
    public class DisablePlayerCollisionSystem : GenericSystemBase, IModSystem
    {
        private static bool _disablePlayerCollisionOption = true;
        public static bool DisablePlayerCollisionOption
        {
            get => _disablePlayerCollisionOption;
            set
            {
                SettingsChanged = true;
                _disablePlayerCollisionOption = value;
            }
        }
        private static bool SettingsChanged = true;
        
        private const int LayerPlayers = 12;
        
        protected override void OnUpdate() {

            if (SettingsChanged)
            {
                Log($"Removing player collisions");
                Physics.IgnoreLayerCollision(LayerPlayers, LayerPlayers, DisablePlayerCollisionOption);
                SettingsChanged = false;
            }
        }
    }
}