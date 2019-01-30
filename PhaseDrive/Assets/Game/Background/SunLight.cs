using UnityEngine;

[ExecuteInEditMode]
public class SunLight: MonoBehaviour
{
    public void Update()
    {
        if (_orbit == null)
            _orbit = FindObjectOfType<Orbit>();

        transform.localRotation = _orbit.light_rotation;
    }

    private Orbit _orbit;
}