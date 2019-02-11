using Assets.Script_Tools;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SceneryCamera : MonoBehaviour
{
    public Matrix4x4 camera_transform;

    public void LateUpdate()
    {
        if (_camera == null) _camera = GetComponent<Camera>();
        if (_orbitalLocation == null) _orbitalLocation = Find.RequiredSingleton<OrbitalLocation>();

        // camera movement
        var so = _orbitalLocation.Orientation;
        var sp = _orbitalLocation.Position;

        var cr = _camera.transform.rotation;
        var C = Matrix4x4.Rotate(so * cr);

        C.SetRow(3, new Vector4(sp.x, sp.y, sp.z, 1));

        camera_transform = C;
    }

    private Camera _camera;
    private OrbitalLocation _orbitalLocation;
}