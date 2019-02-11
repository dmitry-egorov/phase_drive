using UnityEngine;

namespace Assets.Script_Tools
{
    public static class ComponentTryGetComponentExtension
    {
        public static bool TryGetComponent<T>(this Component o, out T c) where T : Component
        {
            return o.GetComponent<T>().TryGetValue(out c);
        }
    }
}