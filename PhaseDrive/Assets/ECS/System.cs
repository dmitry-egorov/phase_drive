using UnityEngine;

namespace Assets.ECS
{
    public abstract class System : MonoBehaviour
    {
        public void Start() { } //just to show the inspector enable flag
        public abstract void Execute();
    }
}