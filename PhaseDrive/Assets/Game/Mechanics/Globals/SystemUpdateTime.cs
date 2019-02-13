using Assets.ECS;
using UnityEngine;

public class SystemUpdateTime: SingletonSystem<Timer>
{
    protected override void Handle(Timer timer)
    {
        if (!timer.Stopped)
        {
            timer.CurrentTime += Time.deltaTime;
        }
    }
}
