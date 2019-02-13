using Assets.ECS;
using UnityEngine;

public class SystemChangeTimeWithPlayerInput : SingletonSystem<Timer>
{
    public float Increment;

    protected override void Handle(Timer timer)
    {
        var i = Increment;
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) i *= 0.1f;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) i *= 10.0f;

        if (Input.GetKeyUp(KeyCode.KeypadPlus))
        {
            timer.CurrentTime += i;
        }

        if (Input.GetKeyUp(KeyCode.KeypadMinus))
        {
            timer.CurrentTime -= i;
        }
    }
}