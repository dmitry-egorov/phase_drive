using Assets.Script_Tools;
using UnityEngine;

namespace Assets.ECS
{
    public abstract class FlagComponent : DataComponent
    {
    }

    public static class GameObjectRaiseFlagExtension
    {
        public static TFlag RaiseFlag<TFlag>(this Component c) where TFlag : FlagComponent => c.gameObject.RaiseFlag<TFlag>();
        public static void ResetFlag<TFlag>(this Component c) where TFlag : FlagComponent => c.gameObject.ResetFlag<TFlag>();
        public static bool TryGetRaisedFlag<TFlag>(this Component c, out TFlag flag) where TFlag : FlagComponent => c.gameObject.TryGetRaisedFlag(out flag);

        public static bool TryGetRaisedFlag<TFlag>(this GameObject go, out TFlag flag)
            where TFlag : FlagComponent
        {
            return go.TryGetComponent(out flag) && flag.isActiveAndEnabled;
        }

        public static bool FlagIsRaised<TFlag>(this GameObject go)
            where TFlag : FlagComponent
        {
            return go.TryGetComponent<TFlag>(out var f) && f.isActiveAndEnabled;
        }

        public static TFlag RaiseFlag<TFlag>(this GameObject go)
            where  TFlag: FlagComponent
        {
            var f = go.GetOrAddTempComponent<TFlag>();
            f.enabled = true;
            return f;
        }

        public static void ResetFlag<TFlag>(this GameObject go)
            where  TFlag: FlagComponent
        {
            var f = go.GetComponent<TFlag>();
            if (f != null)
            {
                f.enabled = false;
            }
        }
    }

}
