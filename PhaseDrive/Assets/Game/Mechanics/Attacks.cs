using System.Collections.Generic;
using Assets.ECS;
using UnityEngine;
using UnityEngine.Serialization;

public class Attacks: DataComponent
{
    [FormerlySerializedAs("Targets")]
    public List<GameObject> TargetsQueue;
}