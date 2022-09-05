using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Root : Node
    {
        public Root()
        {
            name = "root";
        }

        public Root(string name) : base(name)
        {
        }

        public Root(string name, List<Node> children) : base(name, children)
        {
        }

        public override Status Evaluate()
        {
            if (children.Count == 0)
            {
                return Status.SUCCESS;
            }

            return children[CurrentChild].Evaluate();
        }


        public void PrintTree()
        {
            string treePrintOut = "";
            Stack<NodeLevel> nodeStack = new Stack<NodeLevel>();
            Node currentNode = this;
            NodeLevel nextNode = new NodeLevel { level = 0, node = null };
            nodeStack.Push(new NodeLevel { level = 0, node = currentNode });

            while (nodeStack.Count != 0)
            {
                nextNode = nodeStack.Pop();
                treePrintOut += new string('-', nextNode.level) + nextNode.node.GetName() + "\n";

                for (int i = nextNode.node.GetChildren().Count - 1; i >= 0; i--)
                {
                    nodeStack.Push(new NodeLevel { level = nextNode.level + 1, node = nextNode.node.GetChildren()[i] });
                }
            }

            Debug.Log(treePrintOut);
        }
    }
}