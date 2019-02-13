using Assets.ECS;
using UnityEngine;

[ExecuteInEditMode]
public class SystemUpdateOrbitalLocation : SingletonSystem<Timer, OrbitalLocation>
{
    protected override void Handle(Timer t, OrbitalLocation ol)
    {
        // camera movement
        var ct = t.CurrentTime;
        var s = ol.Speed;
        var ra = -ct * s; // rotation angle

        var tl1 = Quaternion.Euler(0, 0, ol.Tilt);
        var so = Quaternion.Euler(0, ra, 0) * tl1; // scene orientation

        var tl2 = Quaternion.Euler(0, 0, -ol.Tilt); // tilt
        var sd = tl2 * Quaternion.Euler(0, -ra, 0); // sun direction

        var d = ol.Distance;
        var sp = so * new Vector3(0, 0, -d);

        ol.Orientation = so;
        ol.Position = sp;
        ol.SunlightDirection = sd;
    }
}