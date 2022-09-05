using System.Collections.Generic;

namespace BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence(string name) : base(name)
        {
        }

        public Sequence(string name, List<Node> children) : base(name, children)
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

            if (childStatus == Status.FAILURE)
            {
                status = Status.FAILURE;
                return status;
            }

            CurrentChild++;
            if (CurrentChild >= children.Count)
            {
                CurrentChild = 0;
                status = Status.SUCCESS;
                return status;
            }

            status = Status.RUNNING;
            return status;
        }
    }
}