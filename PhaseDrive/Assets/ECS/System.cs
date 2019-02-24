using UnityEngine;

namespace Assets.ECS
{
    public abstract class System : MonoBehaviour
    {
        public abstract void Execute();
    }
}