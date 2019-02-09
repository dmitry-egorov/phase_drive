using UnityEngine;

[ExecuteInEditMode]
public class SceneryCamera : MonoBehaviour
{
    public Matrix4x4 camera_transform;

    public void LateUpdate()
    {
        if (_camera == null)
            _camera = GetComponent<Camera>();
        if (_location == null)
            _location = FindObjectOfType<Location>();

        // camera movement
        var so = _location.scene_orientation;
        var sp = _location.scene_position;

        var cr = _camera.transform.rotation;
        var C = Matrix4x4.Rotate(so * cr);

        C.SetRow(3, new Vector4(sp.x, sp.y, sp.z, 1));

        camera_transform = C;
    }

    private Camera _camera;
    private Location _location;
}