using Assets.ECS;
using Assets.Script_Tools;

public class SystemSelectFlagshipWhenNothingIsSelected : SingletonSystem<Selection, LocalPlayer>
{
    protected override void Handle(Selection s, LocalPlayer p)
    {
        if (s.Current != null)
            return;

        var fs = FindObjectsOfType<Flagship>();
        for (var i = 0; i < fs.Length; i++)
        {
            var f = fs[i];
            if
            (
                   f.TryGetComponent<CanBeSelected>(out var canBeSelected)
                && f.TryGetComponent<OwnedBy>(out var ownedBy)
                && ownedBy.Owner == p.gameObject
            )
            {
                s.Select(canBeSelected);
                return;
            }
        }
    }
}