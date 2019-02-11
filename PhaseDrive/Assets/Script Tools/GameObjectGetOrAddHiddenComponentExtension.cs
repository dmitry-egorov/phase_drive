using UnityEngine;

namespace Assets.Script_Tools
{
    public static class GameObjectGetOrAddHiddenComponentExtension
    {
        public static T GetOrAddHiddenComponent<T>(this GameObject obj) where T: Component
        {
            var c = obj.GetComponent<T>(); // component
            if (c == null)
            {
                c = obj.AddComponent<T>();
                c.hideFlags = HideFlags.HideInInspector | HideFlags.DontSave;
            }

            return c;
        }
    }
}