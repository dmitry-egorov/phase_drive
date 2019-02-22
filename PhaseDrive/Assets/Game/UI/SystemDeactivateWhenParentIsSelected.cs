using Assets.ECS;

public class SystemDeactivateWhenParentIsSelected : MultiSystem<InactiveWhenSelected>
{
    protected override void Handle(InactiveWhenSelected m)
    {
        var o = m.gameObject;
        var s = o.GetComponentInParent<CanBeSelected>();

        o.SetActive(s != null && !s.IsSeclected);
    }
}