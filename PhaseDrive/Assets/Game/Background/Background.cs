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
        if (_orbit == null)
            _orbit = FindObjectOfType<Orbit>();

        // camera movement
        var wt = _orbit.camera_rotation;
        var d = distance;
        var o = wt * new Vector3(0, 0, -d);
        var W = Matrix4x4.TRS(o, wt, Vector3.one);

        var c = _camera;
        var V = Matrix4x4.Rotate(c.transform.rotation); // view transform matrix

        var C = W * V;

        C.SetRow(3, new Vector4(o.x, o.y, o.z, 1));

        //C = vtm;

        material.SetMatrix("_CameraTransform", C);
    }

    private Camera _camera;
    private Orbit _orbit;
}
