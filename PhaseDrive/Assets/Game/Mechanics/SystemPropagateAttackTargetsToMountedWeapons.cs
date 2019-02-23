using Assets.ECS;
using UnityEngine.Assertions;

public class SystemPropagateAttackTargetsToMountedWeapons : MultiSystem<PropagatesAttackTargetsToMountedWeapons>
{
    protected override void Handle(PropagatesAttackTargetsToMountedWeapons component)
    {
        var go = component.gameObject;
        var a = go.GetComponent<CanTarget>();
        Assert.IsNotNull(a);

        var t = a.Target;
        if (t == null)
            return;

        var mounts = go.GetChildMounts();

        for (var i = 0; i < mounts.Length; i++)
        {
            var m = mounts[i]; // mount
            var w = m.GetComponentInChildren<CanTarget>(); // weapon
            if (w != null)
            {
                w.Target = t;
            }
        }

        a.Target = null;
    }
}