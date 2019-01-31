using UnityEngine;

[ExecuteInEditMode]
public class Background : MonoBehaviour
{
    public float distance;
    public Material material;

    // Update is called once per frame
    void Update()
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

        material.SetMatrix("_CameraTransform", C);
    }

    private Camera _camera;
    private Location _location;
}
