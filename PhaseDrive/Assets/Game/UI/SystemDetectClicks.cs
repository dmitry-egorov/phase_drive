using Assets.ECS;
using Assets.Script_Tools;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class SystemDetectClicks : Assets.ECS.System
{
    public override void Execute()
    {
        if (mainCamera == null) mainCamera = Find.RequiredSingleton<MainCamera>().GetComponent<Camera>();
        if (emptySpace == null) emptySpace = Find.RequiredSingleton<EmptySpace>().GetComponent<CanBeClicked>();

        var lc = Input.GetKeyUp(KeyCode.Mouse0);
        var rc = Input.GetKeyUp(KeyCode.Mouse1);

        if (!lc && !rc)
            return;

        var b = lc ? MouseButton.LeftMouse : MouseButton.RightMouse;

        if (!RecursiveRaycast(mainCamera, Input.mousePosition, out var clickable))
        {
            clickable = emptySpace;
        }

        var clicked = clickable.RaiseFlag<IsClicked>();
        clicked.MouseButton = b;
    }

    private bool RecursiveRaycast
    (
        Camera camera,
        Vector3 mousePosition,
        out CanBeClicked canBeClicked
    )
    {
        var ray = camera.ScreenPointToRay(mousePosition);

        if (!Physics.Raycast(ray, out var hit))
        {
            canBeClicked = null;
            return false;
        }

        if (!hit.collider.TryGetComponent<Viewport>(out var viewport)) // if it's not a viewport just return the hit
            return hit.collider.TryGetComponent(out canBeClicked);

        //otherwise raycast from the viewport

        var vc = viewport.Camera; // viewport camera
        var p = hit.textureCoord; // normalized point
        var pp = new Vector3(p.x * vc.pixelWidth, p.y * vc.pixelHeight, 0); // point in pixels

        return RecursiveRaycast(vc, pp, out canBeClicked);
    }

    private Camera mainCamera;
    private CanBeClicked emptySpace;
}