using Assets.ECS;
using Assets.Script_Tools;

public class SystemSyncTransform : MultiSystem<HasSameTransformAs>
{
    protected override void Handle(HasSameTransformAs component)
    {
        var rotation = component.gameObject.GetOrAddTempComponent<HasSameRotationAs>();
        var position = component.gameObject.GetOrAddTempComponent<HasSamePositionAs>();

        rotation.Target = component.Target;
        position.Target = component.Target;
    }
}