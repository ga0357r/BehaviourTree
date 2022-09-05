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
                return Status.RUNNING;
            }

            if (childStatus == Status.SUCCESS)
            {
                CurrentChild = 0;
                isShuffled = false;
                return Status.SUCCESS;
            }

            CurrentChild++;
            if (CurrentChild >= children.Count)
            {
                CurrentChild = 0;
                isShuffled = false;
                return Status.FAILURE;
            }

            return Status.RUNNING;
        }
    }
}