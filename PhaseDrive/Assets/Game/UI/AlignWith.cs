using Assets.Script_Tools;
using UnityEngine;

[ExecuteInEditMode]
public class AlignWith : MonoBehaviour
{
    public Transform Target;

    void LateUpdate()
    {
        if (!_initialized)
        {
            _position = gameObject.GetOrAddHiddenComponent<SamePositionAs>();
            _rotation = gameObject.GetOrAddHiddenComponent<SameRotationAs>();

            _initialized = true;
        }

        _position.Target = Target;
        _rotation.Target = Target;
    }

    private SamePositionAs _position;
    private SameRotationAs _rotation;
    private bool _initialized;
}