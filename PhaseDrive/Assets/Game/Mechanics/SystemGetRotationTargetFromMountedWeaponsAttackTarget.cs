using Assets.ECS;
using Assets.Script_Tools;
using UnityEngine;

//note: takes the first weapon that has a target and is mounted in a slot with external alignment
public class SystemGetRotationTargetFromMountedWeaponsAttackTarget : PerObjectSystem<RotatesTowardsWeaponsAttackTarget>
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
                && m.transform.GetChild(0).TryGetComponent<Attacks>(out var a)
                && a.TargetsQueue.Count != 0 // has a target
            )
            {
                target = a.TargetsQueue[0];
                break;
            }
        }

        if (target == null)
            return;

        var rotates = go.GetComponent<Rotates>();
        rotates.TargetRotation = Quaternion.LookRotation(target.transform.position - go.transform.position).eulerAngles;
    }
}