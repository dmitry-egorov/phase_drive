using UnityEngine;

namespace Assets.ECS
{
    /// <summary>
    /// A component that's not affected by the enable flag
    /// Used primarily for activating/deactivating its game object
    /// </summary>
    [ExecuteAlways]
    public abstract class AlwaysOnDataComponent : BaseDataComponent
    {
        protected AlwaysOnDataComponent()
        {
            // the instance will be null during initial load of the scene
            var systemsManager = SystemsManager.Instance;
            if (systemsManager != null) systemsManager.Add(this);
        }

        public void OnDestroy()
        {
            var systemsManager = SystemsManager.Instance;
            if (systemsManager != null) systemsManager.Remove(this);
        }
    }
}