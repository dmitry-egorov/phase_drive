using Assets.ECS;
using Assets.Script_Tools;
using static ViewMode.TheMode;

public class SystemActivateDependingOnViewMode : MultiSystem<ActiveDependingOnViewMode>
{
    protected override void Initialize()
    {
        viewMode = Find.RequiredSingleton<ViewMode>();
    }

    protected override void Handle(ActiveDependingOnViewMode m)
    {
        var a =
                viewMode.Mode == Observational
                    ? m.EnabledInObservationalMode
                    : m.EnabledInTacticalMode
            ;
        m.gameObject.SetActive(a);
    }

    private ViewMode viewMode;
}