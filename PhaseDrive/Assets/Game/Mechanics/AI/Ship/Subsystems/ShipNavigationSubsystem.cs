using Assets.Script_Tools;
using UnityEngine;

public class ShipNavigationSubsystem : MonoBehaviour
{
    public GameObject PossibleShip;
    public Quaternion? PossibleTargetRotation;

    public void SetShip(GameObject ship)
    {
        PossibleShip = ship;
    }

    public void SetTargetRotation(Quaternion? possibleTargetRotation)
    {
        PossibleTargetRotation = possibleTargetRotation;
    }

    public void Update()
    {
        if (!PossibleShip.TryGetValue(out var ship))
            return;

        //TODO: smooth rotation to target
        if (PossibleTargetRotation.TryGetValue(out var rotation))
        {
            ship.transform.rotation = rotation;
        }
    }
}