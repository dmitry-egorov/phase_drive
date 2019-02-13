using UnityEngine;

public class Mount : MonoBehaviour
{
    public bool RequiresShipAlignment;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, 0.2f * Vector3.one);
    }
}