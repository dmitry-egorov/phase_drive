using UnityEngine;

[ExecuteInEditMode]
public class SunLight: MonoBehaviour
{
    void LateUpdate()
    {
        if (_location == null)
            _location = FindObjectOfType<Location>();

        transform.localRotation = _location.sunlight_direction;
    }

    private Location _location;
}