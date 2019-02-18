using Assets.ECS;
using UnityEngine;
using static ViewMode.TheMode;

public class SystemActivateDependingOnViewMode : SingletonSystem<ViewMode>
{
    protected override void Handle(ViewMode viewMode)
    {
        if (markedObjects == null) markedObjects = Resources.FindObjectsOfTypeAll<ActiveDependingOnViewMode>();

        for (var i = 0; i < markedObjects.Length; i++)
        {
            var m = markedObjects[i];
            var a = 
                viewMode.Mode == Observational 
                ? m.EnabledInObservationalMode 
                : m.EnabledInTacticalMode
            ;
            m.gameObject.SetActive(a);
        }
    }

    private ActiveDependingOnViewMode[] markedObjects;
}