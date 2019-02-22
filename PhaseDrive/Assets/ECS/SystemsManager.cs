using System;
using System.Linq;
using Assets.ECS;
using JetBrains.Annotations;
using UnityEngine;

[ExecuteAlways]
public class SystemsManager : MonoBehaviour
{
    [CanBeNull] public static SystemsManager Instance { get; private set; }

    public void Update()
    {
        for (var i = 0; i < systems.Length; i++)
        {
            var s = systems[i];
            s.Execute();
        }
    }

    public void Add(DataComponent dataComponent)
    {
        Init();
        AddUnsafe(dataComponent);
    }

    public void Remove(DataComponent dataComponent)
    {
        Init();

        var t = dataComponent.GetType();

        foreach (var system in multiSystemsLookup[t])
        {
            system.Remove(dataComponent);
        }
    }

    private void AddUnsafe(DataComponent dataComponent)
    {
        var t = dataComponent.GetType();

        foreach (var system in multiSystemsLookup[t])
        {
            system.Add(dataComponent);
        }
    }

    private void Init()
    {
        if (Instance != null)
            return;

        Instance = this;

        // find all systems
        {
            systems = GetComponents<Assets.ECS.System>();
            multiSystemsLookup = 
                GetComponents<MultiSystem>()
                .ToLookup(x => x.GetRootComponentType())
            ;
        }

        // find all "always on" components
        // Note: "always on" components are added in their constructor,
        // but the instance of the systems manager doesn't exist when deserializing
        // so we must find all of them during initialization
        {
            var cs = Resources
                .FindObjectsOfTypeAll<AlwaysOnDataComponent>()
                .Where(x => x.gameObject.scene.rootCount != 0)
                .ToArray()
            ;

            foreach (var c in cs)
            {
                AddUnsafe(c);
            }
        }
    }

    [NonSerialized] private ILookup<Type, MultiSystem> multiSystemsLookup;
    [NonSerialized] private Assets.ECS.System[] systems;
}
