using UnityEngine;

[ExecuteInEditMode]
public class SameRotationAs : MonoBehaviour
{
    public Transform Target;

    void LateUpdate()
    {
        if (Target == null)
            return;

        transform.rotation = Target.rotation;
    }
}