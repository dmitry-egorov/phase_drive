using Assets.ECS;

public class Selection : DataComponent
{
    public CanBeSelected Current;

    public void Select(CanBeSelected selectable)
    {
        Reset();

        selectable.IsSeclected = true;
        Current = selectable;
    }

    public void Reset()
    {
        if (Current == null)
            return;

        Current.IsSeclected = false;
        Current = null;
    }
}