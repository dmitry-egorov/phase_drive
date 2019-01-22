using UnityEngine;

[ExecuteInEditMode]
public class VirtualSpaceBackgroundManager : MonoBehaviour
{
    public float currentTime;
    public float speed;
    public float tilt;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        if (_material == null)
            _material = GetComponent<Skybox>().material;
        if (_camera == null)
            _camera = GetComponent<Camera>();

        if (Application.isPlaying)
        {
            currentTime += Time.deltaTime;
        }

        // camera movement
        var s = speed;

        var tl = Quaternion.Euler(0, 0, tilt); // tilt

        var t = currentTime;
        var ra = -t * s; // rotation angle
        var rt = Quaternion.Euler(0, ra, 0); // rotation

        var wt = rt * tl; // world transform
        var d = distance;
        var o = wt * new Vector3(0, 0, -d);
        var W = Matrix4x4.TRS(o, wt, Vector3.one);

        var c = _camera;
        var V = Matrix4x4.Rotate(c.transform.rotation); // view transform matrix

        var C = W * V;

        C.SetRow(3, new Vector4(o.x, o.y, o.z, 1));

        //C = vtm;

        _material.SetMatrix("_CameraTransform", C);
    }

    private Material _material;
    private Camera _camera;
}
