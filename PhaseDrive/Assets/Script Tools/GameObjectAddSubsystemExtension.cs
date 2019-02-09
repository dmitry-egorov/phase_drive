using UnityEngine;

namespace Assets.Script_Tools
{
    public static class GameObjectAddSubsystemExtension
    {
        public static GameObject AddChild(this GameObject root, string name)
        {
            var o = new GameObject(name);
            o.transform.parent = root.transform;
            return o;
        }

        public static TComponent AddChild<TComponent>(this GameObject root, string name = null) where TComponent : Component
        {
            var o = root.AddChild(name ?? typeof(TComponent).Name);
            return o.AddComponent<TComponent>();
        }
    }
}