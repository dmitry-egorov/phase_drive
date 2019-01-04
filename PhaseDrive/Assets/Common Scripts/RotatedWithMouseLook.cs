using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class RotatedWithMouseLook : MonoBehaviour
{
    public float speed = 1.0f;


    void Start()
    {
        _rotation = transform.eulerAngles;
    }

    void Update()
    {
        if (Input.GetMouseButton((int) MouseButton.RightMouse))
        {

            var dmx = Input.GetAxis("Mouse X");
            var dmy = Input.GetAxis("Mouse Y");

            var d = new Vector2(dmy, dmx) * Time.deltaTime * speed;

            var x = _rotation.x + d.x;
            var y = _rotation.y + d.y;
            if (x > 90f) x = 90f;
            if (x < -90f) x = -90f;

            transform.eulerAngles = new Vector3(x, y, transform.eulerAngles.z);

            _rotation = new Vector2(x, y);
        }
    }

    private Vector2 _rotation;
}
