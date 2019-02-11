using UnityEngine;

[ExecuteInEditMode]
public class LookAt : MonoBehaviour
{
    public Transform Target;

    //Note: late update to prevent frame skipping when it's enabled
    void LateUpdate()
    {
        if (Target == null)
            return;

        transform.LookAt(Target);
    }

    public void SetTarget(Transform target)
    {
        this.Target = target;
        this.LateUpdate();
    }
}