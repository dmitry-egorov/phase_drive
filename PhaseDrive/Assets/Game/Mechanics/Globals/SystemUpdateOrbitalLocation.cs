using Assets.Script_Tools;
using UnityEngine;

[ExecuteInEditMode]
public class SystemUpdateOrbitalLocation : MonoBehaviour
{
    public void Update()
    {
        if (_timer == null) _timer = Find.RequiredSingleton<Timer>();
        if (_orbitalLocation == null) _orbitalLocation = Find.RequiredSingleton<OrbitalLocation>();

        // camera movement
        var t = _timer.CurrentTime;
        var s = _orbitalLocation.Speed;
        var ra = -t * s; // rotation angle

        var tl1 = Quaternion.Euler(0, 0, _orbitalLocation.Tilt);
        var so = Quaternion.Euler(0, ra, 0) * tl1; // scene orientation

        var tl2 = Quaternion.Euler(0, 0, -_orbitalLocation.Tilt); // tilt
        var sd = tl2 * Quaternion.Euler(0, -ra, 0); // sun direction

        var d = _orbitalLocation.Distance;
        var sp = so * new Vector3(0, 0, -d);

        _orbitalLocation.Orientation = so;
        _orbitalLocation.Position = sp;
        _orbitalLocation.SunlightDirection = sd;
    }

    private Timer _timer;
    private OrbitalLocation _orbitalLocation;
}