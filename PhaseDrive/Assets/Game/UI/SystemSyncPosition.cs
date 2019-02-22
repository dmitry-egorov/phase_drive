using Assets.ECS;

public class SystemSyncPosition : MultiSystem<HasSamePositionAs>
{
    protected override void Handle(HasSamePositionAs c)
    {
        var t = c.Target;

        if (t == null)
            return;

        c.transform.position = t.position;
    }
}