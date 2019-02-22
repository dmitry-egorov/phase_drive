using System;
using System.Collections.Generic;
using Assets.Script_Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.ECS
{
    public abstract class DataComponent : MonoBehaviour
    {
    }

    /// <summary>
    /// A component that's not affected by the enable flag
    /// Used primarily for activating/deactivating its game object
    /// </summary>
    [ExecuteAlways]
    public abstract class AlwaysOnDataComponent : DataComponent
    {
        protected AlwaysOnDataComponent()
        {
            // the instance will be null during initial load of the scene
            var systemsManager = SystemsManager.Instance;
            if (systemsManager != null) systemsManager.Add(this);
        }

        public void OnDestroy()
        {
            var systemsManager = SystemsManager.Instance;
            if (systemsManager != null) systemsManager.Remove(this);
        }
    }

    [ExecuteAlways]
    public abstract class OnOffDataComponent : DataComponent
    {
        public void OnEnable()
        {
            if (systemsManager == null) systemsManager = Find.RequiredSingleton<SystemsManager>();
            systemsManager.Add(this);
        }

        public void OnDisable()
        {
            if (systemsManager != null) systemsManager.Remove(this);
        }

        private SystemsManager systemsManager;
    }

    public abstract class System : MonoBehaviour
    {
        public abstract void Execute();
    }

    [ExecuteAlways]
    public abstract class SingletonSystem<TComponent> : System where TComponent : Object
    {
        public override void Execute()
        {
            if (component == null) component = Find.RequiredSingleton<TComponent>();
            Handle(component);
        }

        protected abstract void Handle(TComponent component);

        private TComponent component;
    }

    [ExecuteAlways]
    public abstract class SingletonSystem<TComponent1, TComponent2> : System
        where TComponent1 : Object
        where TComponent2 : Object
    {
        public override void Execute()
        {
            if (component1 == null) component1 = Find.RequiredSingleton<TComponent1>();
            if (component2 == null) component2 = Find.RequiredSingleton<TComponent2>();

            Handle(component1, component2);
        }

        protected abstract void Handle(TComponent1 c1, TComponent2 c2);

        private TComponent1 component1;
        private TComponent2 component2;
    }

    [ExecuteAlways]
    public abstract class SingletonSystem<TComponent1, TComponent2, TComponent3> : System
        where TComponent1 : Object
        where TComponent2 : Object
        where TComponent3 : Object
    {
        public override void Execute()
        {
            if (component1 == null) component1 = Find.RequiredSingleton<TComponent1>();
            if (component2 == null) component2 = Find.RequiredSingleton<TComponent2>();
            if (component3 == null) component3 = Find.RequiredSingleton<TComponent3>();

            Handle(component1, component2, component3);
        }

        protected abstract void Handle(TComponent1 c1, TComponent2 c2, TComponent3 c3);

        private TComponent1 component1;
        private TComponent2 component2;
        private TComponent3 component3;
    }

    public abstract class MultiSystem : System
    {
        // for systems manager
        public abstract Type GetRootComponentType();
        public abstract void Add(DataComponent dataComponent);
        public abstract void Remove(DataComponent dataComponent);

        // for the editor
        public abstract IEnumerable<MonoBehaviour> GetComponents();
        public abstract int GetComponentsCount();
    }

    [ExecuteAlways]
    public abstract class MultiSystem<TRootComponent> : MultiSystem
        where TRootComponent : DataComponent
    {
        public override void Execute()
        {
            Init();

            // Note: "always on" components OnDestroy is not called if they weren't active in their lifetime
            // so we must delete them manually while iterating through components
            var i = 0;
            while (i < components.Count)
            {
                var c = components[i];
                if
                (
                    c == null
#if UNITY_EDITOR
                    // unity calls constructors of prefab components (after the initialization of the scene),
                    // so we clean them up here
                    || c.gameObject.scene.rootCount == 0
#endif
                )
                {
                    RemoveAt(i);
                }
                else
                {
                    Handle(c);
                    i++;
                }
            }
        }

        protected virtual void Initialize()
        {
        }

        protected abstract void Handle(TRootComponent component);

        public override Type GetRootComponentType() => typeof(TRootComponent);

        public override void Add(DataComponent dataComponent)
        {
            Init();
            components.Add((TRootComponent) dataComponent);
        }

        public override void Remove(DataComponent dataComponent)
        {
            Init();
            var i = components.IndexOf((TRootComponent) dataComponent);
            RemoveAt(i);
        }

        public override IEnumerable<MonoBehaviour> GetComponents() => components;
        public override int GetComponentsCount() => components.Count;

        private void RemoveAt(int i)
        {
            var li = components.Count - 1; // last index
            components[i] = components[li];
            components.RemoveAt(li);
        }

        private void Init()
        {
            if (!initialized)
            {
                if (components == null)
                {
                    components = new List<TRootComponent>();
                }
                else
                {
                    components.Clear();
                }

                Initialize();
                initialized = true;
            }
        }

        [NonSerialized] private List<TRootComponent> components;
        [NonSerialized] private bool initialized;
    }
}