using UnityEngine;

public class RotatedWithFixedRate : MonoBehaviour
{
    public float RotationRateDegreesPerSecond;

    void Update()
    {
        transform.Rotate(Vector3.right, RotationRateDegreesPerSecond * Time.deltaTime, Space.Self);
    }
}
