using Assets.ECS;
using UnityEngine;

public class SystemStopTimeWithPlayerInput : SingletonSystem<Timer>
{
    public KeyCode Key = KeyCode.P;

    protected override void Handle(Timer timer)
    {
        if (Input.GetKeyUp(Key))
        {
            timer.Stopped = !timer.Stopped;
        }
    }
}