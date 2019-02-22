using System.Collections.Generic;
using Assets.ECS;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using static Assets.Script_Tools.ShorterVectors;

public class SystemRotateWithMouseLook : MultiSystem<RotatesWithMouseLook>
{
    protected override void Handle(RotatesWithMouseLook rotates)
    {
        if (!Input.GetMouseButton((int) MouseButton.MiddleMouse)) return;

        // x and y are inverted intentionally, since mouse movement in the X axis corresponds to rotation in Y and wise versa
        var dm = v2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        var s = rotates.Speed;
        var dt = Time.deltaTime;
        var d = dm * dt * s;
        var r = rotates.Rotation;

        var a = r + d; // angles
        if (a.x > 89f) a.x = 89f;
        if (a.x < -89f) a.x = -89f;

        var tr = rotates.transform;
        var z = tr.eulerAngles.z;
        tr.eulerAngles = v3(a, z);

        rotates.Rotation = a;
    }
}