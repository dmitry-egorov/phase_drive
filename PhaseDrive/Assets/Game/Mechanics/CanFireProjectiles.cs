using Assets.ECS;
using UnityEngine;

public class CanFireProjectiles: DataComponent
{
    public Transform From;
    public Transform To;
    // allowed magnitude of the difference between weapon orientation and target direction
    public float Margin;
    public float Power;
    public float IgnitionTime;
    public float CooldownTime;
    public GameObject ProjectilePrefab;
}