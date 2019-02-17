using System.Collections.Generic;
using Assets.ECS;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using static Assets.Script_Tools.ShorterVectors;

public class SystemRotateWithMouseLook : MultiSystem<RotatesWithMouseLook>
{
    protected override void Run(List<RotatesWithMouseLook> components)
    {
        if (!Input.GetMouseButton((int) MouseButton.MiddleMouse)) return;

        foreach (var rotated in components)
        {
            // x and y are inverted intentionally, since mouse movement in the X axis corresponds to rotation in Y and wise versa
            var dm = v2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

            var s = rotated.Speed;
            var dt = Time.deltaTime;
            var d = dm * dt * s;
            var r = rotated.Rotation;

            var a = r + d; // angles
            if (a.x > 89f) a.x = 89f;
            if (a.x < -89f) a.x = -89f;

            var tr = rotated.transform;
            var z = tr.eulerAngles.z;
            tr.eulerAngles = v3(a, z);

            rotated.Rotation = a;
        }
    }
}