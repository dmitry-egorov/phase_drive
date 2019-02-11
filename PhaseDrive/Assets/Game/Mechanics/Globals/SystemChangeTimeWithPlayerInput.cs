using Assets.Script_Tools;
using UnityEngine;
using UnityEngine.Serialization;

public class SystemChangeTimeWithPlayerInput : MonoBehaviour
{
    [FormerlySerializedAs("increment")]
    public float Increment;

    public void Update()
    {
        if (_timer == null) _timer = Find.RequiredSingleton<Timer>();

        var i = Increment;
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) i *= 0.1f;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) i *= 10.0f;

        if (Input.GetKeyUp(KeyCode.KeypadPlus))
        {
            _timer.CurrentTime += i;
        }

        if (Input.GetKeyUp(KeyCode.KeypadMinus))
        {
            _timer.CurrentTime -= i;
        }
    }

    private Timer _timer;
}