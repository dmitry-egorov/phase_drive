using System;
using System.Linq;
using Assets.ECS;
using UnityEngine;

public class SystemsManager : MonoBehaviour
{
    public void Add(DataComponent dataComponent)
    {
        Init();

        var t = dataComponent.GetType();

        foreach (var system in systems[t])
        {
            system.Add(dataComponent);
        }
    }

    public void Remove(DataComponent dataComponent)
    {
        Init();

        var t = dataComponent.GetType();

        foreach (var system in systems[t])
        {
            system.Remove(dataComponent);
        }
    }

    private void Init()
    {
        //Note: consider optimizing, maybe using a singleton MonoBehaviour instead of static or just saving some arbitrary reference to a scene object
        if (systems == null)
            systems =
                Resources
                    .FindObjectsOfTypeAll<MonoBehaviour>()
                    .OfType<IMultiSystem>()
                    .ToLookup(x => x.GetRootComponentType())
                ;
    }

    private ILookup<Type, IMultiSystem> systems;
}
