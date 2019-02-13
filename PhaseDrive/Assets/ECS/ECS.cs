using System;
using System.Collections.Generic;
using Assets.Script_Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.ECS
{
    [ExecuteInEditMode]
    public abstract class DataComponent : MonoBehaviour
    {
        public void OnEnable()
        {
            Init();
            _systemsManager.Add(this);
        }

        public void OnDisable()
        {
            Init();
            _systemsManager.Remove(this);
        }

        private void Init()
        {
            if (_systemsManager == null) _systemsManager = Find.RequiredSingleton<SystemsManager>();
        }

        private SystemsManager _systemsManager;
    }

    public abstract class SingletonSystem<TComponent> : MonoBehaviour where TComponent : Object
    {
        public void Update()
        {
            if (component == null) component = Find.RequiredSingleton<TComponent>();

            Handle(component);
        }

        protected abstract void Handle(TComponent component);

        private TComponent component;
    }

    public abstract class SingletonSystem<TComponent1, TComponent2> : MonoBehaviour 
        where TComponent1 : Object
        where TComponent2 : Object
    {
        public void Update()
        {
            if (component1 == null) component1 = Find.RequiredSingleton<TComponent1>();
            if (component2 == null) component2 = Find.RequiredSingleton<TComponent2>();

            Handle(component1, component2);
        }

        protected abstract void Handle(TComponent1 c1, TComponent2 c2);

        private TComponent1 component1;
        private TComponent2 component2;
    }

    public abstract class SingletonSystem<TComponent1, TComponent2, TComponent3> : MonoBehaviour 
        where TComponent1 : Object
        where TComponent2 : Object
        where TComponent3 : Object
    {
        public void Update()
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

    public interface IMultiSystem
    {
        Type GetRootComponentType();
        void Add(DataComponent dataComponent);
        void Remove(DataComponent dataComponent);
    }

    [ExecuteInEditMode]
    public abstract class MultiSystem<TRootComponent>: MonoBehaviour, IMultiSystem
        where TRootComponent : DataComponent
    {
        public void Update()
        {
            Init();
            Run(_components);
        }

        protected virtual void Run(List<TRootComponent> components)
        {
            for (var i = 0; i < components.Count; i++)
            {
                Handle(components[i]);
            }
        }

        protected virtual void Handle(TRootComponent component) { }

        public Type GetRootComponentType() => typeof(TRootComponent);

        public void Add(DataComponent dataComponent)
        {
            Init();
            _components.Add((TRootComponent) dataComponent);
        }

        public void Remove(DataComponent dataComponent)
        {
            Init();
            var i = _components.IndexOf((TRootComponent)dataComponent);
            var li = _components.Count - 1; // last index
            _components[i] = _components[li];
            _components.RemoveAt(li);
        }

        private void Init()
        {
            if (_components == null) _components = new List<TRootComponent>();
        }

        private List<TRootComponent> _components;
    }
}