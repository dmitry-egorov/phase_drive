using Assets.Game.Mechanics.Globals;
using UnityEngine;
using Time = Assets.Game.Mechanics.Globals.Time;

[ExecuteInEditMode]
public class Orbit : MonoBehaviour
{
    public float speed;
    public float tilt;

    public Quaternion camera_rotation;
    public Quaternion light_rotation;

    public void Update()
    {
        if (_time == null)
            _time = FindObjectOfType<Time>();

        // camera movement
        var t = _time.currentTime;
        var s = speed;
        var ra = -t * s; // rotation angle
        var rt = Quaternion.Euler(0, ra, 0);

        var tl1 = Quaternion.Euler(0, 0, tilt); 
        camera_rotation = rt * tl1; // world transform

        var tl2 = Quaternion.Euler(0, 0, -tilt); // tilt
        light_rotation = tl2 * Quaternion.Inverse(rt); // world transform
    }

    private Time _time;
}