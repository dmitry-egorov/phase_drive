using UnityEngine;

[ExecuteInEditMode]
public class Follow : MonoBehaviour
{
    public Transform Target;
    
    void Update()
    {
        if (Target == null)
            return;

        transform.position = Target.position;
    }
}
