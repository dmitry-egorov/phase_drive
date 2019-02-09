using UnityEngine;

public class TimeChanger : MonoBehaviour
{
    public float increment;

    public void Update()
    {
        if (_timeKeeper == null) _timeKeeper = GetComponent<TimeKeeper>();

        if (Input.GetKeyUp(KeyCode.KeypadPlus))
        {
            _timeKeeper.currentTime += increment;
        }

        if (Input.GetKeyUp(KeyCode.KeypadMinus))
        {
            _timeKeeper.currentTime -= increment;
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            _timeKeeper.enabled = !_timeKeeper.enabled;
        }
    }

    private TimeKeeper _timeKeeper;
}