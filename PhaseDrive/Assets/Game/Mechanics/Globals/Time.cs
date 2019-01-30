using UnityEngine;

namespace Assets.Game.Mechanics.Globals
{
    public class Time: MonoBehaviour
    {
        public float currentTime;

        public void Update()
        {
            currentTime += UnityEngine.Time.deltaTime;
        }
    }
}