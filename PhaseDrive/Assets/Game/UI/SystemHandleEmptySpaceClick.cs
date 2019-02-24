using Assets.ECS;
using UnityEngine.Experimental.UIElements;

public class SystemHandleEmptySpaceClick: SingletonSystem<EmptySpace, Selection>
{
    protected override void Handle(EmptySpace c, Selection s)
    {
        if (c.TryGetRaisedFlag<IsClicked>(out var cl) && cl.MouseButton == MouseButton.LeftMouse)
        {
            s.Reset();
        }
    }
}