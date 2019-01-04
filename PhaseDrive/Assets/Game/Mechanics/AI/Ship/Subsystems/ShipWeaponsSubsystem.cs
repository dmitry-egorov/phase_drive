using Assets.ScriptTools;
using UnityEngine;

public class ShipWeaponsSubsystem : MonoBehaviour
{
    public GameObject PossibleShip;

    public void SetShip(GameObject ship)
    {
        PossibleShip = ship;
    }

    public Quaternion? GetOptimalRotation()
    {
        //TODO: get some combination of all targets
        if (!PossibleShip.TryGetValue(out var ship))
            return default;

        var weapons = ship.GetComponentsInChildren<WeaponAI>();

        if (!weapons.TryGetFirstItem(out var firstWeapon))
            return default;

        if (!firstWeapon.GetPossibleFirstTarget().TryGetValue(out var target))
            return default;

        return Quaternion.LookRotation(target.transform.position - ship.transform.position);
    }

    public void Update()
    {

    }
}