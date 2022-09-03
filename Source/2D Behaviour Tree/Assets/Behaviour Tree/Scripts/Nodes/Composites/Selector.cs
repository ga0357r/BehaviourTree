using System.Collections.Generic;

public class Selector : Node
{
    public Selector(string name) : base(name)
    {
    }

    public Selector(string name, List<Node> children) : base(name, children)
    {
    }

    public override Status Evaluate()
    {
        Status childStatus = children[CurrentChild].Evaluate();

        if (childStatus == Status.RUNNING)
        {
            return Status.RUNNING;
        }

        if (childStatus == Status.SUCCESS)
        {
            CurrentChild = 0;
            return Status.SUCCESS;
        }

        CurrentChild++;
        if (CurrentChild >= children.Count)
        {
            CurrentChild = 0;
            return Status.FAILURE;
        }

        return Status.RUNNING;
    }
}
