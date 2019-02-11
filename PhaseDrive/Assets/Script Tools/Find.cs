using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Script_Tools
{
    public static class Find
    {
        public static T RequiredSingleton<T>() where T : Object
        {
            var obj = Object.FindObjectOfType<T>();
            Assert.IsNotNull(obj, $"{typeof(T).Name} not found");
            return obj;
        }

        public static T1 RequiredSingleton<T1, T2>() where T1 : Component where T2 : Component
        {
            var objs = Object.FindObjectsOfType<T1>();

            Assert.IsTrue(objs.Length != 0, $"{typeof(T1).Name} not found");

            var obj = objs.FirstOrDefault(o => o.GetComponent<T2>() != null);
            Assert.IsNotNull(obj, $"Object with {typeof(T1).Name} and {typeof(T2).Name} not found");

            return obj;
        }
    }
}