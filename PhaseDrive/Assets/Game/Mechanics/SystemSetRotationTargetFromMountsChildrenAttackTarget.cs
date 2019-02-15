using Assets.ECS;
using Assets.Script_Tools;
using UnityEngine;

public class SystemSetRotationTargetFromMountsChildrenAttackTarget : MultiSystem<RotatesTowardsMountsChildrenAttackTarget>
{
    protected override void Handle(RotatesTowardsMountsChildrenAttackTarget component)
    {
        var go = component.gameObject;

        var mounts = go.GetChildMounts();
        var target = (GameObject)null;

        for (var i = 0; i < mounts.Length; i++)
        {
            var m = mounts[i]; // mount
            if 
            (
                m.ExternalAlignment
                && m.transform.childCount != 0 
                && m.transform.GetChild(0).TryGetComponent<Attacks>(out var a) 
                && a.Targets.Count != 0
            )
            {
                target = a.Targets[0];
                break;
            }
        }

        if (target == null)
            return;

        var rotates = go.GetComponent<Rotates>();
        rotates.TargetRotation = Quaternion.LookRotation(target.transform.position - go.transform.position).eulerAngles;
    }
}