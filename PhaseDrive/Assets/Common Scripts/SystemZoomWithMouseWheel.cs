using Assets.ECS;
using Assets.Script_Tools;
using UnityEngine;
using static UnityEngine.Mathf;

public class SystemZoomWithMouseWheel : MultiSystem<ZoomsWithMouseWheel>
{
    protected override void Handle(ZoomsWithMouseWheel zoomed)
    {
        var cz = zoomed.CurrentZoom;
        var tz = zoomed.TargetZoom;
        var sp = zoomed.Speed;
        var tr = zoomed.transform;

        var nz = Lerp(tz, cz, Pow(0.5f, sp)); // new zoom
        zoomed.CurrentZoom = nz;
        tr.localPosition = tr.localPosition.WithZ(-nz);

        var d = Input.mouseScrollDelta.y;
        if (d == 0.0)
            return;

        var sn = zoomed.Sensitivity;
        tz += -Sign(d) * sn;

        var min = zoomed.Min;
        var max = zoomed.Max;
        tz = Clamp(tz, min, max);

        zoomed.TargetZoom = tz;
    }
}