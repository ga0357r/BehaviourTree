using System.Collections.Generic;
using UnityEngine;

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
            return Status.RUNNING;
        }

        if (childStatus == Status.SUCCESS)
        {
            return Status.FAILURE;
        }

        return Status.SUCCESS;
    }
}
