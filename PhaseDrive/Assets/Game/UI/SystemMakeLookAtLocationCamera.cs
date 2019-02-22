using Assets.ECS;
using Assets.Script_Tools;

public class SystemMakeLookAtLocationCamera : MultiSystem<LooksAtLocationCamera>
{
    protected override void Handle(LooksAtLocationCamera l)
    {
        var looksAt = l.gameObject.GetOrAddTempComponent<LooksAt>();
        var location = l.gameObject.GetComponentInParent<Location>();

        looksAt.Target = location?.Camera?.transform;
    }
}