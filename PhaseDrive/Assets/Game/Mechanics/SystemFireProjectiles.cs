using Assets.ECS;
using Assets.Script_Tools;
using UnityEngine;

public class SystemFireProjectiles : MultiSystem<FiresProjectiles>
{
    public Transform ProjectilesParent;

    protected override void Initialize()
    {
        timer = Find.RequiredSingleton<Timer>();
    }

    protected override void Handle(FiresProjectiles fp)
    {
        var go = fp.gameObject;
        var a = go.GetComponent<CanTarget>();
        if (a == null || a.Target == null)
            return;

        var t = a.Target.transform.position; // current target position
        var from = fp.From.position;
        var to = fp.To.position;

        var dt = (t - from).normalized; // direction to target
        var dw = (to - from).normalized; // weapon direction

        var ca = Vector3.Dot(dt, dw); // cos of the angle between target direction and weapon direction

        var m = fp.Margin;
        if (ca < 1f - m) // angle is too wide
            return;

        var time = timer.CurrentTime;

        if (go.TryGetComponent<Igniting>(out var i))
        {
            var it = fp.IgnitionTime;
            var ist = i.StartTime; // ignition start time
            if (it > time - ist) // still igniting
                return;

            Destroy(i); // remove ignition component

            // create a projectile
            {
                var p = Instantiate(fp.ProjectilePrefab, ProjectilesParent);
                var mass = p.GetComponent<Massive>().Mass;
                var mv = p.GetComponent<Moves>();

                p.transform.LookAt(to);

                var pow = fp.Power;
                mv.Velocity = (pow / mass) * dw;
            }

            // start cooldown
            {
                var nc = go.AddComponent<Cooling>(); // new cooldown
                nc.StartTime = time;
            }

            return;
        }

        if (go.TryGetComponent<Cooling>(out var c))
        {
            var ct = fp.CooldownTime;
            var cst = c.StartTime;

            if (ct > time - cst) // still on cooldown
                return;

            Destroy(c);
        }

        // start ignition
        {
            var ni = go.AddComponent<Igniting>(); // new igniting
            ni.StartTime = time;
        }
    }

    private Timer timer;
}