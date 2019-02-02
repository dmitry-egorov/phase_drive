using UnityEngine;

[ExecuteInEditMode]
public class Background : MonoBehaviour
{
    public Material material;

    // Update is called once per frame
    void Update()
    {
        if (_camera == null)
            _camera = FindObjectOfType<SceneryCamera>();

        var C = _camera.camera_transform;
        material.SetMatrix("_CameraTransform", C);
    }

    private SceneryCamera _camera;
}
