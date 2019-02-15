using System.Collections.Generic;
using Assets.ECS;
using Assets.Script_Tools;

public class SystemMove : MultiSystem<Moves>
{
    protected override void Run(List<Moves> components)
    {
        if (timer == null) timer = Find.RequiredSingleton<Timer>();

        for (var i = 0; i < components.Count; i++)
        {
            var m = components[i]; // moves

            var dt = timer.DeltaTime;
            var tr = m.transform;
            var v = m.Velocity;

            tr.position += v * dt;
        }
    }

    private Timer timer;
}