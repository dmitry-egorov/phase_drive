using Assets.ScriptTools;
using UnityEngine;

public class ZoomWithMouseWheel : MonoBehaviour
{
    public float sensitivity = 0.5f;
    public float speed = 1.0f;
    public float min = 700;
    public float max = 3000;

    public void Update()
    {
        var cp = transform.localPosition; // current position
        var cz = cp.z; // current zoom

        if (target_distance.TryGetValue(out var td))
        {
            var nz = Mathf.Lerp(td, cz, Mathf.Pow(0.5f, sensitivity)); // new zoom
            transform.localPosition = new Vector3(cp.x, cp.y, nz);
        }

        var d = Input.mouseScrollDelta.y;
        if (d == 0.0)
            return;

        var min = -this.max;
        var max = -this.min;

        var tz = target_distance ?? cz;
        tz += d * speed;
        target_distance = Mathf.Clamp(tz, min, max);
    }

    private float? target_distance;

}