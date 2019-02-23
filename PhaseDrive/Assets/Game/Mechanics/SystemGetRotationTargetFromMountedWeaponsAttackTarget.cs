using Assets.ECS;
using Assets.Script_Tools;
using UnityEngine;

//note: takes the first weapon that has a target and is mounted in a slot with external alignment
public class SystemGetRotationTargetFromMountedWeaponsAttackTarget : MultiSystem<RotatesTowardsWeaponsAttackTarget>
{
    protected override void Handle(RotatesTowardsWeaponsAttackTarget component)
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
                && m.transform.childCount != 0 // has mounted weapon
                && m.transform.GetChild(0).TryGetComponent<CanTarget>(out var a)
                && a.Target != null // has a target
            )
            {
                target = a.Target;
                break;
            }
        }

        if (target == null)
            return;

        var rotates = go.GetComponent<CanRotate>();
        rotates.TargetRotation = Quaternion.LookRotation(target.transform.position - go.transform.position).eulerAngles;
    }
}