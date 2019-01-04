using UnityEngine;

[ExecuteInEditMode]
public class SyncWorldRotation : MonoBehaviour
{
    public Transform Target;
    
    void Update()
    {
        if (Target != null)
        {
            transform.rotation = Target.rotation;
        }
    }
}