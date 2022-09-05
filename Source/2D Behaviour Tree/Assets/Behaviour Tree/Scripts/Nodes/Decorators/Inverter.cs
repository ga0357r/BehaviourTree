using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Inverter : Node
    {
        public Inverter(string name) : base(name)
        {
        }

        public Inverter(string name, List<Node> children) : base(name, children)
        {
        }

        public override Status Evaluate()
        {
            if (CurrentChild != 0)
            {
                Debug.LogError("Decorator Nodes are only allowed to have 1 child");
                return Status.FAILURE;
            }

            Status childStatus = children[CurrentChild].Evaluate();

            if (childStatus == Status.RUNNING)
            {
                status = Status.RUNNING;
                return status;
            }

            if (childStatus == Status.SUCCESS)
            {
                status = Status.FAILURE;
                return status;
            }

            status = Status.SUCCESS;
            return status;
        }
    }
}