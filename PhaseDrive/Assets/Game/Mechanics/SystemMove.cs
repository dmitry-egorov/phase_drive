using Assets.ECS;
using Assets.Script_Tools;

public class SystemMove : MultiSystem<Moves>
{
    protected override void Initialize()
    {
        timer = Find.RequiredSingleton<Timer>();
    }

    protected override void Handle(Moves m)
    {
        var dt = timer.DeltaTime;
        var tr = m.transform;
        var v = m.Velocity;

        tr.position += v * dt;
    }

    private Timer timer;
}