using Assets.Script_Tools;
using UnityEngine;

namespace Assets.ECS
{
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
}