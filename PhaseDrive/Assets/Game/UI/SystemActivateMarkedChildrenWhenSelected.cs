using Assets.ECS;
using Assets.Script_Tools;
using UnityEngine;

public class SystemActivateMarkedChildrenWhenSelected : PerObjectSystem<CanBeSelected>
{
    protected override void Handle(CanBeSelected selectable)
    {
        var cache = selectable.gameObject.GetOrAddTempComponent<Cache>();
        cache.Init();

        //Note: Marked children will be activated or deactivated depending on the selection status and the type of the marking component
        var isSelected = selectable.IsSeclected;
        ProcessActives(isSelected, cache.Actives);
        ProcessInactives(isSelected, cache.Inactives);
    }

    private static void ProcessInactives(bool isSelected, InactiveWhenSelected[] inactives)
    {
        for (var i = 0; i < inactives.Length; i++)
        {
            var o = inactives[i].gameObject;
            o.SetActive(!isSelected);
        }
    }

    private static void ProcessActives(bool isSelected, ActiveWhenSelected[] actives)
    {
        for (var i = 0; i < actives.Length; i++)
        {
            var o = actives[i].gameObject;
            o.SetActive(isSelected);
        }
    }

    private class Cache: DataComponent
    {
        public ActiveWhenSelected[] Actives;
        public InactiveWhenSelected[] Inactives;

        public void Init()
        {
            if (Actives == null) Actives = GetComponentsInChildren<ActiveWhenSelected>(true);
            if (Inactives == null) Inactives = GetComponentsInChildren<InactiveWhenSelected>(true);
        }
    }
}
