using System.Collections.Generic;

namespace BehaviourTree
{
    public class RandomSelector : Selector
    {
        private bool isShuffled = false;

        public RandomSelector(string name) : base(name)
        {
        }

        public RandomSelector(string name, List<Node> children) : base(name, children)
        {
        }

        public override Status Evaluate()
        {
            if (!isShuffled)
            {
                children.Shuffle();
                isShuffled = true;
            }

            Status childStatus = children[CurrentChild].Evaluate();

            if (childStatus == Status.RUNNING)
            {
                status = Status.RUNNING;
                return status;
            }

            if (childStatus == Status.SUCCESS)
            {
                CurrentChild = 0;
                isShuffled = false;
                status = Status.SUCCESS;
                return status;
            }

            CurrentChild++;
            if (CurrentChild >= children.Count)
            {
                CurrentChild = 0;
                isShuffled = false;
                status = Status.FAILURE;
                return status;
            }

            status = Status.RUNNING;
            return status;
        }
    }
}