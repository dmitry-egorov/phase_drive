using Assets.Script_Tools;
using UnityEngine;

public class ShipAI : MonoBehaviour
{
    public GameObject Ship;

    void Update()
    {
        InitializeOnce();

        var possibleRotation = _weapons.GetOptimalRotation();
        _navigation.SetTargetRotation(possibleRotation);
    }

    private void InitializeOnce()
    {
        if (_initialized)
            return;

        _weapons = gameObject.AddChild<ShipWeaponsSubsystem>("Weapons");
        _navigation = gameObject.AddChild<ShipNavigationSubsystem>("Navigation");

        _weapons.SetShip(Ship);
        _navigation.SetShip(Ship);

        _initialized = true;
    }

    private bool _initialized;
    private ShipNavigationSubsystem _navigation;
    private ShipWeaponsSubsystem _weapons;
}