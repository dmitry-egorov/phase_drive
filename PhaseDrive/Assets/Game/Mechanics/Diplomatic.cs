﻿using System;
using Assets.ECS;
using UnityEngine;

public class Diplomatic : DataComponent
{
    public Relationship[] Relationships;

    [Serializable]
    public struct Relationship
    {
        public GameObject Entity;
        public Status_ Status;

        public enum Status_
        {
              Hostile = 0
            , Neutral = 10
            , Friendly = 20
        }
    }
}