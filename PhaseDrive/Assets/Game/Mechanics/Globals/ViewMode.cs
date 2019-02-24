using Assets.ECS;

public class ViewMode : DataComponent
{
    public TheMode Mode;

    public enum TheMode
    {
        Tactical = 0,
        Observational = 1
    }
}