using System.Collections.Generic;
using Assets.ECS;
using UnityEngine;
using UnityEngine.Serialization;

public class CanAttack: OnOffDataComponent
{
    [FormerlySerializedAs("Targets")]
    public List<GameObject> TargetsQueue;
}