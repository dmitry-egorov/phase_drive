using Assets.ECS;

public class SystemMakeLookAt: MultiSystem<LooksAt>
{
    protected override void Handle(LooksAt looksAt)
    {
        looksAt.transform.LookAt(looksAt.Target);
    }
}