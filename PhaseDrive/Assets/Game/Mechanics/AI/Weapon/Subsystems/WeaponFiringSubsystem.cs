using UnityEngine;

public class WeaponFiringSubsystem : MonoBehaviour
{
    public GameObject PossibleTarget;

    void Update()
    {
        //TODO: fire at the current target
    }

    public void SetPossibleTarget(GameObject possibleTarget)
    {
        PossibleTarget = possibleTarget;
    }
}