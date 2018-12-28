using Assets.ScriptTools;
using UnityEngine;

public class ShipAI : MonoBehaviour
{
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

        var subsystemsRoot = gameObject.AddChild("AI Subsystems");

        _weapons = subsystemsRoot.AddChild<ShipWeaponsSubsystem>("Weapons");
        _navigation = subsystemsRoot.AddChild<ShipNavigationSubsystem>("Navigation");

        _weapons.SetShip(gameObject);
        _navigation.SetShip(gameObject);

        _initialized = true;
    }

    private bool _initialized;
    private ShipNavigationSubsystem _navigation;
    private ShipWeaponsSubsystem _weapons;
}