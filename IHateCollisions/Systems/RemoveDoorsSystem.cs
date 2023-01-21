using System.Collections.Generic;
using System.Linq;

namespace DoorsAreAnnoying.Systems
{
    public class RemoveDoorsSystem : GenericSystemBase, IModSystem
    {
        private static bool _removeDoorsOption = true;
        public static bool RemoveDoorsOption
        {
            get => _removeDoorsOption;
            set
            {
                SettingsChanged = true;
                _removeDoorsOption = value;
            }
        }
        private static bool SettingsChanged;

        private List<GameObject> Doors = new List<GameObject>();

        private void RemoveDoors()
        {
            if (RemoveDoorsOption)
            {
                Log($"{(RemoveDoorsOption ? "remove" : "re-create")} doors in scene");
            }
            var allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;

            if (!Doors.Any())
            {
                Doors = allObjects.Where(o => o.name.StartsWith("Door")).ToList();
            }
            
            foreach (var door in Doors)
            {
                door.SetActive(!RemoveDoorsOption);
            }
        }
        
        private SceneType LastScene = SceneType.Null;

        // TODO: doors are renderd some frames in
        private bool firstRenderSkipped;
        
        protected override void OnUpdate()
        {
            if (!firstRenderSkipped)
            {
                firstRenderSkipped = true;
                return;
            }
            
            if (LastScene != GameInfo.CurrentScene || SettingsChanged)
            {
                SettingsChanged = false;
                LastScene = GameInfo.CurrentScene;

                if (LastScene is SceneType.Franchise or SceneType.Kitchen)
                {
                    RemoveDoors();
                }
            }
        }
    }
}