using Assets.ECS;
using Assets.Script_Tools;
using UnityEngine;

public class SystemMakeLookAtLocationCamera : PerObjectSystem<LooksAtLocationCamera>
{
    protected override void Handle(LooksAtLocationCamera component)
    {
        var cache = component.gameObject.GetOrAddTempComponent<Cache>();
        cache.Init();

        cache.LooksAt.Target = cache.Location?.Camera?.transform;
    }

    private class Cache: MonoBehaviour
    {
        public Location Location;
        public LooksAt LooksAt;

        public void Init()
        {
            if (Location == null) Location = gameObject.GetComponentInParent<Location>();
            if (LooksAt == null) LooksAt = gameObject.GetOrAddTempComponent<LooksAt>();
        }
    }
}