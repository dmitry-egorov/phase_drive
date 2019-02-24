using Assets.ECS;
using Assets.Script_Tools;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class SystemHandleLeftClickOnACommandedEntity : MultiSystem<IsClicked>
{
    protected override void Initialize()
    {
        localPlayer = Find.RequiredSingleton<LocalPlayer>().gameObject;
        selection = Find.RequiredSingleton<Selection>();
    }

    protected override void Handle(IsClicked c)
    {
        if
            (
                // left clicked a selectable entity, commanded by the local player
                c.MouseButton == MouseButton.LeftMouse
                && c.TryGetComponent<CanBeSelected>(out var selectable)
                && c.TryGetComponent<OwnedBy>(out var ownable)
                && ownable.Owner == localPlayer
            )
            // select the clicked entity
        {
            selection.Select(selectable);
        }
    }

    private GameObject localPlayer;
    private Selection selection;
}