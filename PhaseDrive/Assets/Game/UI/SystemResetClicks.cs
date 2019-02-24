using Assets.ECS;

public class SystemResetClicks : MultiSystem<CanBeClicked>
{
    protected override void Handle(CanBeClicked component)
    {
        component.ResetFlag<IsClicked>();
    }
}