using Assets.ECS;
using UnityEngine;
using UnityEngine.Serialization;

public class OrbitalLocation : OnOffDataComponent
{
    [FormerlySerializedAs("distance")] public float Distance;
    [FormerlySerializedAs("speed")]    public float Speed;
    [FormerlySerializedAs("tilt")]     public float Tilt;

    public Quaternion Orientation;
    public Vector3 Position;
    public Quaternion SunlightDirection;
}