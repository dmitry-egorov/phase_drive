using UnityEngine;

[ExecuteInEditMode]
public class LookAt : MonoBehaviour
{
    public Transform Target;

    void Update()
    {
        if (Target == null)
            return;

        transform.LookAt(Target);
    }
}