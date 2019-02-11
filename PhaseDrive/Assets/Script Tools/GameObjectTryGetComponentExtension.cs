using UnityEngine;

namespace Assets.Script_Tools
{
    public static class GameObjectTryGetComponentExtension
    {
        public static bool TryGetComponent<T>(this GameObject o, out T c) where T : Component
        {
            return o.GetComponent<T>().TryGetValue(out c);
        }
    }
}