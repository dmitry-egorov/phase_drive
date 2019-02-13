using Assets.ECS;

public class SystemSyncRotation : MultiSystem<HasSameRotationAs>
{
    protected override void Handle(HasSameRotationAs marked)
    {
        var t = marked.Target;

        if (t == null)
            return;

        marked.transform.rotation = t.rotation;
    }
}