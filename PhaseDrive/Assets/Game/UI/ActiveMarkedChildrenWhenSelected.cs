using UnityEngine;

[RequireComponent(typeof(Selectable))]
public class ActiveMarkedChildrenWhenSelected : MonoBehaviour
{
    void Update()
    {
        if (_selectable == null) _selectable = GetComponent<Selectable>();
        if (_objectsToActivate == null) _objectsToActivate = GetComponentsInChildren<ActivatedOnSelection>(true);
        if (_objectsToDeactivate == null) _objectsToDeactivate = GetComponentsInChildren<DeactivatedOnSelection>(true);

        //Note: Marked children will be activated or deactivated depending on the selection status and the type of the marking component
        var isSelected = _selectable.IsSeclected;
        ProcessActivated(isSelected);
        ProcessDeactivated(isSelected);
    }

    private void ProcessDeactivated(bool isSelected)
    {
        for (var i = 0; i < _objectsToDeactivate.Length; i++)
        {
            var o = _objectsToDeactivate[i].gameObject;
            o.SetActive(!isSelected);
        }
    }

    private void ProcessActivated(bool isSelected)
    {
        for (var i = 0; i < _objectsToActivate.Length; i++)
        {
            var o = _objectsToActivate[i].gameObject;
            o.SetActive(isSelected);
        }
    }

    private Selectable _selectable;
    private ActivatedOnSelection[] _objectsToActivate;
    private DeactivatedOnSelection[] _objectsToDeactivate;
}
