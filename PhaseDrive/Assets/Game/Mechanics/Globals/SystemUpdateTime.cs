using Assets.ECS;
using UnityEngine;

public class SystemUpdateTime: SingletonSystem<Timer>
{
    protected override void Handle(Timer timer)
    {
        if (!timer.Stopped)
        {
            var dt = Time.deltaTime;

            timer.DeltaTime = dt;
            timer.CurrentTime += dt;
        }
        else
        {
            timer.DeltaTime = 0.0f;
        }
    }
}
