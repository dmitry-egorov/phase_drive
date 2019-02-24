using Assets.Script_Tools;
using UnityEngine;

namespace Assets.ECS
{
    [ExecuteAlways]
    public abstract class DataComponent : BaseDataComponent
    {
        public void OnEnable()
        {
            if (systemsManager == null) systemsManager = Find.RequiredSingleton<SystemsManager>();
            systemsManager.Add(this);
        }

        public void OnDisable()
        {
            if (systemsManager != null) systemsManager.Remove(this);
        }

        private SystemsManager systemsManager;
    }
}