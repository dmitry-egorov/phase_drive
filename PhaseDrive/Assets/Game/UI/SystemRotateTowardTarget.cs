using Assets.ECS;
using UnityEngine;

public class SystemRotateTowardTarget : MultiSystem<CanRotate>
{
    protected override void Handle(CanRotate r)
    {
        var t = r.transform;
        t.rotation = Quaternion.RotateTowards(t.rotation, Quaternion.Euler(r.TargetRotation), r.Speed * Time.deltaTime);
    }
}