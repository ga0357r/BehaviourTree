using UnityEngine;

namespace BehaviourTree
{
    public class Leaf : Node
    {
        public delegate Status Tick();
        private Tick action;

        public delegate Status TickMulti(int index);
        private TickMulti actionMulti;
        public int index;

        public Leaf(string name, Tick action)
        {
            this.name = name;
            this.action = action;
        }

        public Leaf(string name, int index, TickMulti actionMulti)
        {
            this.name = name;
            this.index = index;
            this.actionMulti = actionMulti;
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

            else if (actionMulti != null)
            {
                return actionMulti(index);
            }
            
            Debug.Log(name + " " + status);
            return Status.FAILURE;
            
        }
    }
}