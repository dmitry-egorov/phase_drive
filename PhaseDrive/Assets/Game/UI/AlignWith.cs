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
            _position = gameObject.GetOrAddTempComponent<HasSamePositionAs>();
            _rotation = gameObject.GetOrAddTempComponent<HasSameRotationAs>();

            _initialized = true;
        }

        _position.Target = Target;
        _rotation.Target = Target;
    }

    private HasSamePositionAs _position;
    private HasSameRotationAs _rotation;
    private bool _initialized;
}