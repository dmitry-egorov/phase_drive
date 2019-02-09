using UnityEngine;

[ExecuteInEditMode]
public class Location : MonoBehaviour
{
    public float distance;
    public float speed;
    public float tilt;

    public Quaternion scene_orientation;
    public Vector3 scene_position;
    public Quaternion sunlight_direction;

    public void Update()
    {
        if (_time == null)
            _time = FindObjectOfType<TimeKeeper>();

        // camera movement
        var t = _time.currentTime;
        var s = speed;
        var ra = -t * s; // rotation angle

        var tl1 = Quaternion.Euler(0, 0, tilt); 
        var so = Quaternion.Euler(0, ra, 0) * tl1; // scene orientation

        var tl2 = Quaternion.Euler(0, 0, -tilt); // tilt
        var sd = tl2 * Quaternion.Euler(0, -ra, 0); // sun direction

        var d = distance;
        var sp = so * new Vector3(0, 0, -d);

        scene_orientation = so;
        scene_position = sp;
        sunlight_direction = sd;
    }

    private TimeKeeper _time;
}