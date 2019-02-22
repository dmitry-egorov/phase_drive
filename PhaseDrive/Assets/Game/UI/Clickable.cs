using Assets.ECS;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Clickable : OnOffDataComponent
{
    public GameObject Root;
}
