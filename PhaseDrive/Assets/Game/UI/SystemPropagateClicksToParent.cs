using Assets.ECS;
using UnityEngine.Assertions;

public class SystemPropagateClicksToParent : MultiSystem<IsClicked>
{
    protected override void Handle(IsClicked component)
    {
        //note: support recursion??

        var canBeClicked = component.GetComponent<CanBeClicked>();
        if (!canBeClicked.PropagatesClicksToParent)
            return;

        var p = component.transform.parent.GetComponentInParent<CanBeClicked>();
        Assert.IsNotNull(p, "\"Propagates clicks to parent\" flag is set, but no parent with \"Can be clicked\" component is found");

        var c = p.RaiseFlag<IsClicked>();
        c.MouseButton = component.MouseButton;

        canBeClicked.ResetFlag<IsClicked>();
    }
}