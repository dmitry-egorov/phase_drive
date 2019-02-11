using Assets.Script_Tools;
using UnityEngine;

public static class IsHostileTowardsExtension
{
    public static bool IsHostileTowards(this GameObject self, GameObject other)
    {
        if (!self.TryGetComponent<Diplomatic>(out var diplomatic))
            return false;

        for (var i = 0; i < diplomatic.Relationships.Length; i++)
        {
            var r = diplomatic.Relationships[i];
            if (r.Entity == other)
                return r.Status == Diplomatic.Relationship.Status_.Hostile;
        }

        return false;
    }
}