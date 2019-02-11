using UnityEngine;

[ExecuteInEditMode]
public class SamePositionAs : MonoBehaviour
{
    public Transform Target;
    
    void LateUpdate()
    {
        if (Target == null)
            return;

        transform.position = Target.position;
    }
}