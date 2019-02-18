using Assets.ECS;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Clickable : DataComponent
{
    public GameObject Root;
}
