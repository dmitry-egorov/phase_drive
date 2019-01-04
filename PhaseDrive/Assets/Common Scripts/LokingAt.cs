using UnityEngine;

[ExecuteInEditMode]
public class LokingAt : MonoBehaviour
{
    public Transform Target;
    
    void Update()
    {
        transform.LookAt(Target);
    }
}
