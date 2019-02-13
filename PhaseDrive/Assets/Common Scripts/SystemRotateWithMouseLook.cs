using System.Collections.Generic;
using Assets.ECS;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using static Assets.Script_Tools.ShorterVectors;

public class SystemRotateWithMouseLook : MultiSystem<RotatesWithMouseLook>
{
    protected override void Run(List<RotatesWithMouseLook> components)
    {
        if (!Input.GetMouseButton((int) MouseButton.RightMouse)) return;

        foreach (var rotated in components)
        {
            var dm = v2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            var s = rotated.Speed;
            var dt = Time.deltaTime;
            var d = dm * dt * s;
            var r = rotated.Rotation;

            var a = r + d; // angles
            if (a.x > 89f) a.x = 89f;
            if (a.x < -89f) a.x = -89f;

            var z = transform.eulerAngles.z;
            transform.eulerAngles = v3(a, z);

            rotated.Rotation = a;
        }
    }
}