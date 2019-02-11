using UnityEngine;

public class ZoomedWithMouseWheel : MonoBehaviour
{
    public float Sensitivity = 1.0f;
    public float Speed = 0.3f;
    public float Min = 700;
    public float Max = 3000;

    public float CurrentZoom;
    public float TargetZoom;
}