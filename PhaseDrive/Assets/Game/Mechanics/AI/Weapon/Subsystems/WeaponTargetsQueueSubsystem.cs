using UnityEngine;

public class WeaponTargetsQueueSubsystem: MonoBehaviour
{
    public GameObject GetPossibleFirstTarget()
    {
        if (transform.childCount == 0)
            return default;

        return transform
            .GetChild(0)
            .GetComponent<WeaponTarget>()
            .Object
        ;
    }

    void Update()
    {
        while (transform.childCount > 0)
        {
            var target = 
                transform
                .GetChild(0)
                .GetComponent<WeaponTarget>()
            ;

            var targetObject = target.Object;

            if (targetObject == null || targetObject.GetComponent<Destructable>().IsDestroyed)
            {
                Destroy(target.gameObject);
            }
            else
            {
                break;
            }
        }
    }
}