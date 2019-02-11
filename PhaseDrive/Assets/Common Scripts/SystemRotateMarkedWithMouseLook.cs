using Assets.Script_Tools;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[ExecuteInEditMode]
public class SystemRotateMarkedWithMouseLook : MonoBehaviour
{
    public float Speed = 1.0f;

    void Update()
    {
        if (_rotated == null) _rotated = Find.RequiredSingleton<RotatedWithMouseLook>();

        if (Input.GetMouseButton((int)MouseButton.RightMouse))
        {
            var dmx = Input.GetAxis("Mouse X");
            var dmy = Input.GetAxis("Mouse Y");

            var d = new Vector2(dmy, dmx) * Time.deltaTime * Speed;
            var r = _rotated.Rotation;

            var x = r.x + d.x;
            var y = r.y + d.y;
            if (x > 89f) x = 89f;
            if (x < -89f) x = -89f;

            transform.eulerAngles = new Vector3(x, y, transform.eulerAngles.z);

            _rotated.Rotation = new Vector2(x, y);
        }
    }

    private RotatedWithMouseLook _rotated;
}