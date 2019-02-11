using System;
using System.Collections.Generic;
using UnityEngine;
using static Controlable.Command.Action_;

public class Controlable: MonoBehaviour
{
    public bool CanAttack;
    public List<Command> Commands;

    [Serializable]
    public struct Command
    {
        public GameObject Target;
        public Action_ Action;

        public enum Action_
        {
            Attack
        }
    }

    public void IssueAttack(GameObject target)
    {
        if (Commands == null) Commands = new List<Command>(4);

        Commands.Clear();
        Commands.Add(new Command {Action = Attack, Target = target});
    }
}
