using Assets.Script_Tools;
using UnityEngine;

[ExecuteInEditMode]
public class SystemZoomMarkedWithMouseWheel : MonoBehaviour
{
    public void Update()
    {
        if (_zoomed == null) _zoomed = Find.RequiredSingleton<ZoomedWithMouseWheel>();

        var cz = _zoomed.CurrentZoom;
        var tz = _zoomed.TargetZoom;
        var sp = _zoomed.Speed;
        var t = _zoomed.transform;

        var nz = Mathf.Lerp(tz, cz, Mathf.Pow(0.5f, sp)); // new zoom
        _zoomed.CurrentZoom = nz;
        t.localPosition = t.localPosition.WithZ(-nz);

        var d = Input.mouseScrollDelta.y;
        if (d == 0.0)
            return;

        var sn = _zoomed.Sensitivity;
        tz += d * sn;

        var min = -_zoomed.Max;
        var max = -_zoomed.Min;
        _zoomed.TargetZoom = Mathf.Clamp(tz, min, max);
    }

    private ZoomedWithMouseWheel _zoomed;
}