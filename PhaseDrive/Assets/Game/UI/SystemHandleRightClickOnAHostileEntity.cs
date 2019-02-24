using Assets.ECS;
using Assets.Script_Tools;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class SystemHandleRightClickOnAHostileEntity : MultiSystem<IsClicked>
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
                // right clicked a hostile entity
                c.MouseButton == MouseButton.RightMouse
                && selection.Current != null
                && selection.Current.TryGetComponent<CanTarget>(out var canTarget)
                && c.TryGetComponent<OwnedBy>(out var owned)
                && localPlayer.IsHostileTowards(owned.Owner)
            )
            // attack the clicked entity with currently selected units
        {
            canTarget.Target = c.gameObject;
        }
    }

    private GameObject localPlayer;
    private Selection selection;
}