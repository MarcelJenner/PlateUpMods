using System.Linq;
using Kitchen;
using KitchenMods;
using UnityEngine;

namespace NoClipDoors
{
    public class NoClipSystem : GenericSystemBase, IModSystem {

        private const int LayerPlayers = 12;
        
        // private void LogGameObjects()
        // {
        //     var allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
        //     for (int i = 0; i < allObjects.Length; i++)
        //     {
        //         var gameObject = allObjects[i];
        //         Debug.Log($"NoClipDoors Mod: Gameobject {i}: {gameObject.GetType()} - {gameObject.name} - {gameObject.tag}");
        //     }
        // }

        private void Log(string message)
        {
            Debug.Log($"NoClipDoors Mod: {message}");
        }

        private void SceneChanged()
        {
            Log($"remove door colliders in scene");

            Physics.IgnoreLayerCollision(LayerPlayers, LayerPlayers, true);
            
            var allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;

            foreach (var door in allObjects.Where(o => o.name.StartsWith("Door")))
            {
                door.SetActive(false);
            }
        }
        
        private SceneType LastScene = SceneType.Null;

        private int frameCount;
        
        protected override void OnUpdate() {

            if (frameCount < 500)
            {
                frameCount++;
                return;
            }
            
            if (LastScene != GameInfo.CurrentScene)
            {
                Log($"changed scene {LastScene} --> {GameInfo.CurrentScene}");
                LastScene = GameInfo.CurrentScene;

                if (LastScene == SceneType.Franchise || LastScene == SceneType.Kitchen)
                {
                    SceneChanged();
                }
            }
        }
    }
}