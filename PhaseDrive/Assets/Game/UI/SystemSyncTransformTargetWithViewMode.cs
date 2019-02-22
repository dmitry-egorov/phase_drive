using Assets.ECS;
using Assets.Script_Tools;

public class SystemSyncTransformTargetWithViewMode : MultiSystem<ChangesTransformSyncWithViewMode>
{
    protected override void Handle(ChangesTransformSyncWithViewMode c)
    {
        if (viewMode == null) viewMode = Find.RequiredSingleton<ViewMode>();

        var vm = viewMode.Mode;
        var t = c.gameObject.GetOrAddTempComponent<HasSameTransformAs>();
        t.Target = vm == ViewMode.TheMode.Observational ? c.ObservationalTransform : c.TacticalTransform;
    }

    private ViewMode viewMode;
}