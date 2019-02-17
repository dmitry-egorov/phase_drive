using Assets.ECS;
using UnityEngine;
using static ViewMode.TheMode;

public class SystemToggleViewMode : SingletonSystem<ViewMode>
{
    public KeyCode Key = KeyCode.F;

    protected override void Handle(ViewMode m)
    {
        if (Input.GetKeyUp(Key))
        {
            m.Mode = m.Mode == Observational
                ? Tactical
                : Observational
            ;
        }
    }
}