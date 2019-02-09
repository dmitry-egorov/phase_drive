using Assets.ScriptTools;
using Assets.Script_Tools;
using UnityEngine;

public class WeaponAI : MonoBehaviour
{
    public bool IsStationary; //TODO: Can't target subsystems

    public GameObject GetPossibleFirstTarget() =>
        _initialized 
        ? _targetsQueue.GetPossibleFirstTarget() 
        : null
    ;

    void Update()
    {
        InitializeOnce();

        var possibleTarget = _targetsQueue.GetPossibleFirstTarget();
        _firing.SetPossibleTarget(possibleTarget);
    }

    private void InitializeOnce()
    {
        if (_initialized)
            return;

        var subsystemsRoot = gameObject.AddChild("AI Subsystems");

        _targetsQueue = subsystemsRoot.AddChild<WeaponTargetsQueueSubsystem>("Targets Queue");
        _firing = subsystemsRoot.AddChild<WeaponFiringSubsystem>("Firing");

        _initialized = true;
    }

    private bool _initialized;
    private WeaponFiringSubsystem _firing;
    private WeaponTargetsQueueSubsystem _targetsQueue;
}