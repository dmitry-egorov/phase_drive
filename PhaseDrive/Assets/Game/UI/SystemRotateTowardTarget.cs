using Assets.ECS;
using UnityEngine;

public class SystemRotateTowardTarget : PerObjectSystem<Rotates>
{
    protected override void Handle(Rotates r)
    {
        var t = r.transform;
        t.rotation = Quaternion.RotateTowards(t.rotation, Quaternion.Euler(r.TargetRotation), r.Speed * Time.deltaTime);
    }
}