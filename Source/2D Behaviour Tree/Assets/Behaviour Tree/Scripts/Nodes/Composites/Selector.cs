using System.Collections.Generic;

namespace BehaviourTree
{
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
                status = Status.RUNNING;
                return status;
            }

            if (childStatus == Status.SUCCESS)
            {
                CurrentChild = 0;
                status = Status.SUCCESS;
                return status;
            }

            CurrentChild++;
            if (CurrentChild >= children.Count)
            {
                CurrentChild = 0;
                status = Status.FAILURE;
                return status;
            }

            status = Status.RUNNING;
            return status;
        }
    }
}