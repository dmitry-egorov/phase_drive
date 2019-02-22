using Assets.ECS;

public class SystemSyncRotation : MultiSystem<HasSameRotationAs>
{
    protected override void Handle(HasSameRotationAs c)
    {
        var t = c.Target;

        if (t == null)
            return;

        c.transform.rotation = t.rotation;
    }
}