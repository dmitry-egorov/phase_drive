using Assets.ECS;
using UnityEngine;

public class SystemStopTimeWithPlayerInput : SingletonSystem<Timer>
{
    protected override void Handle(Timer timer)
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            timer.Stopped = !timer.Stopped;
        }
    }
}