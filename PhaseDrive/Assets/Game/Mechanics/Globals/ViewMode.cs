using UnityEngine;

public class ViewMode : MonoBehaviour
{
    public TheMode Mode;

    public enum TheMode
    {
        Tactical = 0,
        Observational = 1
    }
}