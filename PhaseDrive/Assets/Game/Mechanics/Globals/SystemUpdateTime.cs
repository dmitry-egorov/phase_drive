using Assets.Script_Tools;
using UnityEngine;

public class SystemUpdateTime: MonoBehaviour
{
    public void Update()
    {
        if (_timer == null) _timer = Find.RequiredSingleton<Timer>();

        if (!_timer.Stopped)
        {
            _timer.CurrentTime += Time.deltaTime;
        }
    }

    private Timer _timer;
}
