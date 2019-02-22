using Assets.ECS;
using UnityEngine;
using UnityEngine.Serialization;

public class Mount : OnOffDataComponent
{
    [FormerlySerializedAs("RequiresShipAlignment")]public bool ExternalAlignment;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, 0.2f * Vector3.one);
    }
}