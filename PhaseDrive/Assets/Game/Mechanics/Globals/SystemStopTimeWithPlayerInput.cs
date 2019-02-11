using Assets.Script_Tools;
using UnityEngine;

public class SystemStopTimeWithPlayerInput : MonoBehaviour
{
    public void Update()
    {
        if (_timer == null) _timer = Find.RequiredSingleton<Timer>();

        if (Input.GetKeyUp(KeyCode.P))
        {
            _timer.Stopped = !_timer.Stopped;
        }
    }

    private Timer _timer;
}