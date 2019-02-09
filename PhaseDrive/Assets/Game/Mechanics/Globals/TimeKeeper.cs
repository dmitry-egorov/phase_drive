using UnityEngine;

public class TimeKeeper: MonoBehaviour
{
    public float currentTime;

    public void Update()
    {
        currentTime += Time.deltaTime;
    }
}
