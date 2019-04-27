using Assets.Script_Tools;
using UnityEngine;

public static class MountTools
{
    public static CanMount[] GetChildMounts(this GameObject go)
    {
        // get or populate mounts cache
        var mc = go.GetOrAddTempComponent<MountsCache>();
        if (!mc.Mounts.TryGetValue(out var mounts)) mounts = mc.Mounts = go.GetComponentsInChildren<CanMount>();
        return mounts;
    }
}