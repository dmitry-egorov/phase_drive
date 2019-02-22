using Assets.ECS;
using Assets.Script_Tools;
using UnityEngine;

public class Background : OnOffDataComponent
{
    public Material material;

    void LateUpdate()
    {
        if (_camera == null) _camera = Find.RequiredSingleton<SceneryCamera>();

        var C = _camera.camera_transform;
        material.SetMatrix("_CameraTransform", C);
    }

    private SceneryCamera _camera;
}
