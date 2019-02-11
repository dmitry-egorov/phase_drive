using Assets.Script_Tools;
using UnityEngine;

[ExecuteInEditMode]
public class SystemUpdateSunlightRotation : MonoBehaviour
{
    void Update()
    {
        if (_orbitalLocation == null) _orbitalLocation = Find.RequiredSingleton<OrbitalLocation>();
        if (_sunLight == null) _sunLight = Find.RequiredSingleton<SunLight>();

        _sunLight.transform.localRotation = _orbitalLocation.SunlightDirection;
    }

    private OrbitalLocation _orbitalLocation;
    private SunLight _sunLight;
}