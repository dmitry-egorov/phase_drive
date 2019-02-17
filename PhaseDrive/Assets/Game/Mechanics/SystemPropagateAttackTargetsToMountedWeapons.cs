using Assets.ECS;
using UnityEngine.Assertions;

public class SystemPropagateAttackTargetsToMountedWeapons : PerObjectSystem<PropagatesAttackTargetsToMountedWeapons>
{
    protected override void Handle(PropagatesAttackTargetsToMountedWeapons component)
    {
        var go = component.gameObject;
        var a = go.GetComponent<Attacks>();
        Assert.IsNotNull(a);

        var t = a.TargetsQueue;
        if (t.Count == 0)
            return;

        var mounts = go.GetChildMounts();

        for (var i = 0; i < mounts.Length; i++)
        {
            var m = mounts[i]; // mount
            var w = m.GetComponentInChildren<Attacks>(); // weapon
            if (w != null)
            {
                w.TargetsQueue.Clear();
                w.TargetsQueue.AddRange(t);
            }
        }

        t.Clear();
    }
}