using Assets.ECS;
using UnityEngine.Assertions;

public class SystemPropagateAttackTargetsToMountsChildren : MultiSystem<PropagatesAttackTargetsToMountsChildren>
{
    protected override void Handle(PropagatesAttackTargetsToMountsChildren component)
    {
        var go = component.gameObject;
        var a = go.GetComponent<Attacks>();
        Assert.IsNotNull(a);

        var t = a.Targets;
        if (t.Count == 0)
            return;

        var mounts = go.GetChildMounts();

        for (var i = 0; i < mounts.Length; i++)
        {
            var m = mounts[i]; // mount
            var w = m.GetComponentInChildren<Attacks>(); // weapon
            if (w != null)
            {
                w.Targets.Clear();
                w.Targets.AddRange(t);
            }
        }

        t.Clear();
    }
}