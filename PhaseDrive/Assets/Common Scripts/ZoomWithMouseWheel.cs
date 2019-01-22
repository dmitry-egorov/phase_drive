using UnityEngine;

public class ZoomWithMouseWheel : MonoBehaviour
{
    public float speed = 1.0f;
    public float min = 700;
    public float max = 3000;

    public void Update()
    {
        var d = Input.mouseScrollDelta.y;
        if (d == 0.0)
            return;

        var min = -this.max;
        var max = -this.min;

        var p = transform.localPosition;
        var z = p.z;
        z += d * speed;
        z = Mathf.Clamp(z, min, max);
        transform.localPosition = new Vector3(p.x, p.y, z);
    }
}