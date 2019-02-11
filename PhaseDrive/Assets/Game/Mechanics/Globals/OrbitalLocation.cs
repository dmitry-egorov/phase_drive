using UnityEngine;
using UnityEngine.Serialization;

public class OrbitalLocation : MonoBehaviour
{
    [FormerlySerializedAs("distance")] public float Distance;
    [FormerlySerializedAs("speed")]    public float Speed;
    [FormerlySerializedAs("tilt")]     public float Tilt;

    public Quaternion Orientation;
    public Vector3 Position;
    public Quaternion SunlightDirection;
}