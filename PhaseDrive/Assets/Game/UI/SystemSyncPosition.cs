using Assets.ECS;

public class SystemSyncPosition : MultiSystem<HasSamePositionAs>
{
    protected override void Handle(HasSamePositionAs component)
    {
        var t = component.Target;

        if (t == null)
            return;

        transform.position = t.position;
    }
}