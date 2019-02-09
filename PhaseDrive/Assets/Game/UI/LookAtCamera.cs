using UnityEngine;

[ExecuteInEditMode]
public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        if (!_initialized)
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
                return;

            var lookAt = GetComponent<LookAt>();
            if (lookAt == null)
            {
                lookAt = gameObject.AddComponent<LookAt>();
                lookAt.hideFlags = HideFlags.HideInInspector | HideFlags.DontSave;
            }

            lookAt.Target = mainCamera.transform;

            _initialized = true;
        }
    }


    private bool _initialized;
}