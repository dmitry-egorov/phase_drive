using Assets.Script_Tools;
using UnityEngine;

[ExecuteInEditMode]
public class LookAtDispositionCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (_location == null) _location = gameObject.GetComponentInParent<Location>();
        if (_lookAt == null) _lookAt = gameObject.GetOrAddHiddenComponent<LookAt>();

        _lookAt.SetTarget(_location?.Camera?.transform);
    }

    private Location _location;
    private LookAt _lookAt;
}