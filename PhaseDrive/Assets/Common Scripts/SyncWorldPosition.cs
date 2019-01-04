using UnityEngine;

[ExecuteInEditMode]
public class SyncWorldPosition : MonoBehaviour
{
    public Transform Target;
    
    void Update()
    {
        if (Target != null)
        {
            transform.position = Target.position;
        }
    }
}