using Assets.ScriptTools;
using JetBrains.Annotations;
using UnityEngine;

public class CommandsManager : MonoBehaviour
{
    public void Update()
    {
        if (_camera == null) _camera = Camera.main;

        if (Input.GetMouseButtonUp(0))
        {
            var ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            if 
            (
                Physics.Raycast(ray1, out var hit) 
                && hit.collider.GetComponent<ClickRoot>().TryGetValue(out var clickDetector)
            )
            {
                HandleObjectClick(clickDetector.Root);
            }
            else
            {
                HandleClickEmptySpace();
            }
        }
    }

    private void HandleClickEmptySpace()
    {
        Deselect();
    }

    private void HandleObjectClick(GameObject obj)
    {
        if (obj.GetComponent<Selectable>().TryGetValue(out var selectable))
        {
            Deselect();

            selectable.IsSeclected = true;
            _currentSelection = selectable;
        }
    }

    private void Deselect()
    {
        if (!_currentSelection.TryGetValue(out var lastSelectable))
            return;

        lastSelectable.IsSeclected = false;
        _currentSelection = null;
    }

    [CanBeNull] private Camera _camera;
    [CanBeNull] private Selectable _currentSelection;
}
