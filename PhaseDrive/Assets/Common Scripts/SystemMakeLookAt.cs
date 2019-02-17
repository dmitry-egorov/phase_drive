using Assets.ECS;

public class SystemMakeLookAt: PerObjectSystem<LooksAt>
{
    protected override void Handle(LooksAt looksAt)
    {
        looksAt.transform.LookAt(looksAt.Target);
    }
}