using Assets.Script_Tools;
using UnityEngine;
using static ViewMode.TheMode;

public class SystemEnableDependingOnViewMode : MonoBehaviour
{
    public void Update()
    {
        if (viewMode == null) viewMode = Find.RequiredSingleton<ViewMode>();
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

    private ViewMode viewMode;
    private ActiveDependingOnViewMode[] markedObjects;
}