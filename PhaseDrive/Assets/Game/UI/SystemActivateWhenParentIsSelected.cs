﻿using Assets.ECS;

public class SystemActivateWhenParentIsSelected : MultiSystem<ActiveWhenSelected>
{
    protected override void Handle(ActiveWhenSelected m)
    {
        var o = m.gameObject;
        var s = o.GetComponentInParent<CanBeSelected>();

        o.SetActive(s != null && s.IsSeclected);
    }
}