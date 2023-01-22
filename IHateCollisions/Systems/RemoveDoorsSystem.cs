using System.Collections.Generic;
using System.Linq;

namespace IHateCollisions.Systems;

public class RemoveDoorsSystem : GenericSystemBase, IModSystem
{
    private static bool _removeDoorsOption = true;
    public static bool RemoveDoorsOption
    {
        get => _removeDoorsOption;
        set
        {
            _settingsChanged = true;
            _removeDoorsOption = value;
        }
    }
    private static bool _settingsChanged;

    private List<GameObject> _doors = new List<GameObject>();
    private SceneType _lastScene = SceneType.Null;
    private int _frameCount;

    private void RemoveDoors()
    {
        Log($"{(RemoveDoorsOption ? "remove" : "re-create")} doors in scene");

        foreach (var door in _doors)
        {
            door.SetActive(!RemoveDoorsOption);
        }
    }
        
    protected override void OnUpdate()
    {
        // TODO: i dont like this
        if (_frameCount < 500)
        {
            _frameCount++;
            return;
        }

        if (_lastScene != GameInfo.CurrentScene)
        {
            _lastScene = GameInfo.CurrentScene;

            if (GameInfo.CurrentScene is SceneType.Franchise or SceneType.Kitchen)
            {
                _doors = Object.FindObjectsOfType<GameObject>().Where(o => o.name.StartsWith("Door")).ToList();
                RemoveDoors();
            }
        }
            
        if (_settingsChanged)
        {
            _settingsChanged = false;
            RemoveDoors();
        }
    }
}