using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ECS
{
    public abstract class MultiSystem : System
    {
        // for systems manager
        public abstract Type GetRootComponentType();
        public abstract void Add(BaseDataComponent dataComponent);
        public abstract void Remove(BaseDataComponent dataComponent);

        // for the editor
        public abstract IEnumerable<MonoBehaviour> GetComponents();
        public abstract int GetComponentsCount();
    }

    [ExecuteAlways]
    public abstract class MultiSystem<TRootComponent> : MultiSystem
        where TRootComponent : BaseDataComponent
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

        public override void Add(BaseDataComponent dataComponent)
        {
            Init();
            components.Add((TRootComponent)dataComponent);
        }

        public override void Remove(BaseDataComponent dataComponent)
        {
            Init();
            var i = components.IndexOf((TRootComponent)dataComponent);
            RemoveAt(i);
        }

        public override IEnumerable<MonoBehaviour> GetComponents() => components;
        public override int GetComponentsCount() => components?.Count ?? 0;

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