using UnityEngine;

[RequireComponent(typeof(Selectable))]
public class ActivateMarkedChildrenWhenSelected : MonoBehaviour
{
    void Update()
    {
        if (_selectable == null) _selectable = GetComponent<Selectable>();
        if (_actives == null) _actives = GetComponentsInChildren<ActiveWhenSelected>(true);
        if (_inactives == null) _inactives = GetComponentsInChildren<InactiveWhenSelected>(true);

        //Note: Marked children will be activated or deactivated depending on the selection status and the type of the marking component
        var isSelected = _selectable.IsSeclected;
        ProcessActives(isSelected);
        ProcessInactives(isSelected);
    }

    private void ProcessInactives(bool isSelected)
    {
        for (var i = 0; i < _inactives.Length; i++)
        {
            var o = _inactives[i].gameObject;
            o.SetActive(!isSelected);
        }
    }

    private void ProcessActives(bool isSelected)
    {
        for (var i = 0; i < _actives.Length; i++)
        {
            var o = _actives[i].gameObject;
            o.SetActive(isSelected);
        }
    }

    private Selectable _selectable;
    private ActiveWhenSelected[] _actives;
    private InactiveWhenSelected[] _inactives;
}
