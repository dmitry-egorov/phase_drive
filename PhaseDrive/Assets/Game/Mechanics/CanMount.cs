using Assets.ECS;
using UnityEngine;

public class CanMount : DataComponent
{
    public bool ExternalAlignment;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, 0.2f * Vector3.one);
    }
}