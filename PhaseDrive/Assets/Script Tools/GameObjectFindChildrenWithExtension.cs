using System.Linq;
using UnityEngine;

namespace Assets.Script_Tools
{
    public static class GameObjectFindChildrenWithExtension
    {
        public static GameObject[] FindChildrenWith<TComponent>(this GameObject root)
            where TComponent : Component 
        => 
            root
                .GetComponentsInChildren<TComponent>()
                .Select(c => c.gameObject)
                .ToArray()
        ;
    }
}