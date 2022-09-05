namespace BehaviourTree
{
    public class Leaf : Node
    {
        public delegate Status Tick();
        private Tick action;

        public Leaf(string name, Tick action)
        {
            this.name = name;
            this.action = action;
        }

        public Leaf(string name, Tick action, int priority)
        {
            this.name = name;
            this.action = action;
            Priority = priority;
        }

        public override Status Evaluate()
        {
            if (action != null)
            {
                return action();
            }

            return Status.FAILURE;
        }
    }
}