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
                return Status.RUNNING;
            }

            if (childStatus == Status.FAILURE)
            {
                return childStatus;
            }

            CurrentChild++;
            if (CurrentChild >= children.Count)
            {
                CurrentChild = 0;
                return Status.SUCCESS;
            }

            return Status.RUNNING;
        }
    }
}