using System.Collections.Generic;
using Assets.ECS;
using Assets.Script_Tools;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class SystemHandlePlayerCommands : MonoBehaviour
{
    public void Update()
    {
        if (_camera == null) _camera = Find.RequiredSingleton<Camera, Main>();
        if (_localPlayer == null) _localPlayer = Find.RequiredSingleton<LocalPlayer>().gameObject;

        var lc = Input.GetMouseButtonUp((int)MouseButton.LeftMouse);
        var rc = Input.GetMouseButtonUp((int)MouseButton.RightMouse);

        if (!lc && !rc)
            return;

        if
        (
            RecursiveRaycast(_camera, Input.mousePosition, out var clickable)
        )
        {
            var b = lc ? MouseButton.LeftMouse : MouseButton.RightMouse;
            HandleObjectClick(clickable.Root, b);
        }
        else
        {
            HandleEmptyClick();
        }
    }

    private bool RecursiveRaycast
    (
        Camera camera, 
        Vector3 mousePosition, 
        out Clickable clickable
    )
    {
        var ray = camera.ScreenPointToRay(mousePosition);

        if (!Physics.Raycast(ray, out var hit))
        {
            clickable = null;
            return false;
        }

        if (!hit.collider.TryGetComponent<Viewport>(out var viewport)) // if it's not a viewport just return the hit
            return hit.collider.TryGetComponent(out clickable);

        //otherwise raycast from the viewport

        var vc = viewport.Camera; // viewport camera
        var p = hit.textureCoord; // normalized point
        var pp = new Vector3(p.x * vc.pixelWidth, p.y * vc.pixelHeight, 0); // point in pixels

        return RecursiveRaycast(vc, pp, out clickable);
    }

    private void HandleEmptyClick()
    {
        Deselect();
    }

    private void HandleObjectClick(GameObject clickedEntity, MouseButton mouseButton)
    {
        if 
        (
            // left clicked a selectable entity, commanded by the local player
            mouseButton == MouseButton.LeftMouse
            && clickedEntity.TryGetComponent<Selectable>(out var selectable)
            && clickedEntity.TryGetComponent<Ownable>(out var ownable)
            && ownable.Owner == _localPlayer
        )
            // select the clicked entity
        {
            Deselect();

            selectable.IsSeclected = true;
            _currentSelection = selectable;
            return;
        }

        if
        (
            // right clicked a hostile entity
               mouseButton == MouseButton.RightMouse
            && _currentSelection.TryGetValue(out var selection)
            && selection.TryGetComponent<Controlable>(out var controlable)
            && controlable.CanAttack
            && clickedEntity.TryGetComponent(out ownable)
            && _localPlayer.IsHostileTowards(ownable.Owner)
        )
            // attack the clicked entity with currently selected units
        {
            IssueAttack(controlable, clickedEntity);
            return;
        }

    }

    private void Deselect()
    {
        if (!_currentSelection.TryGetValue(out var lastSelectable))
            return;

        lastSelectable.IsSeclected = false;
        _currentSelection = null;
    }

    private static void IssueAttack(Controlable controlable, GameObject target)
    {
        var go = controlable.gameObject;

        // get or populate weapons cache
        var wc = go.GetOrAddTempComponent<WeaponsCache>(); // weapons cache
        if (!wc.Weapons.TryGetValue(out var weapons)) weapons = wc.Weapons = go.GetComponentsInChildren<Weapon>();

        for (var i = 0; i < weapons.Length; i++)
        {
            var w = weapons[i];
            w.TargetsQueue.Clear();
            w.TargetsQueue.Add(target);
        }
    }

    private class WeaponsCache: MonoBehaviour
    {
        public Weapon[] Weapons;
    }

    private Camera _camera;
    private GameObject _localPlayer;

    [CanBeNull] private Selectable _currentSelection;
}