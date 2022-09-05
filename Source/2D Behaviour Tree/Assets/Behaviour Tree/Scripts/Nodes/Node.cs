using System.Collections.Generic;

namespace BehaviourTree
{
    public class Node
    {
        protected string name;
        protected Status status;
        protected List<Node> children = new List<Node>();
        public int CurrentChild { get; set; }
        public int Priority { get; set; }

        public enum Status
        {
            FAILURE,
            SUCCESS,
            RUNNING
        }

        public Node()
        {

        }

        public Node(string name)
        {
            this.name = name;
        }

        public Node(string name, List<Node> children)
        {
            this.name = name;
            this.children = children;
        }

        public Node(string name, int priority)
        {
            this.name = name;
            Priority = priority;
        }

        public virtual Status Evaluate()
        {
            status = Status.SUCCESS;
            return status;
        }

        public void AddChild(Node child)
        {
            children.Add(child);
        }

        public string GetName()
        {
            return name;
        }

        public List<Node> GetChildren()
        {
            return children;
        }

        public Status GetStatus()
        {
            return status;
        }
    }
}