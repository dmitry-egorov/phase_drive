using UnityEngine;

public class Mount : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, 3f * Vector3.one);
    }
}