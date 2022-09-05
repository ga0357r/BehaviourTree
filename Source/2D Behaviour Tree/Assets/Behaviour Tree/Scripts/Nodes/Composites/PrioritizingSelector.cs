using System.Collections.Generic;

namespace BehaviourTree
{
    public class PrioritizingSelector : Selector
    {
        private bool isPrioritized = false;
        private Node[] prioritizedChildren;

        public PrioritizingSelector(string name) : base(name)
        {
        }

        public PrioritizingSelector(string name, List<Node> children) : base(name, children)
        {
        }

        private void PrioritizeNodes()
        {
            prioritizedChildren = children.ToArray();
            Sort(prioritizedChildren, 0, children.Count - 1);
            children = new List<Node>(prioritizedChildren);
        }

        int Partition(Node[] array, int low, int high)
        {
            Node pivot = array[high];

            int lowIndex = (low - 1);

            //2. Reorder the collection.
            for (int j = low; j < high; j++)
            {
                if (array[j].Priority <= pivot.Priority)
                {
                    lowIndex++;

                    Node temp = array[lowIndex];
                    array[lowIndex] = array[j];
                    array[j] = temp;
                }
            }

            Node temp1 = array[lowIndex + 1];
            array[lowIndex + 1] = array[high];
            array[high] = temp1;

            return lowIndex + 1;
        }

        void Sort(Node[] array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(array, low, high);
                Sort(array, low, partitionIndex - 1);
                Sort(array, partitionIndex + 1, high);
            }
        }

        public override Status Evaluate()
        {
            if (!isPrioritized)
            {
                PrioritizeNodes();
                isPrioritized = true;
            }
            

            Status childStatus = children[CurrentChild].Evaluate();

            if (childStatus == Status.RUNNING)
            {
                status = Status.RUNNING;
                return status;
            }

            if (childStatus == Status.SUCCESS)
            {
                children[CurrentChild].Priority = 1;
                CurrentChild = 0;
                isPrioritized = false;
                status = Status.SUCCESS;
                return status;
            }

            else
            {
                children[CurrentChild].Priority = 10;
            }

            CurrentChild++;
            if (CurrentChild >= children.Count)
            {
                CurrentChild = 0;
                isPrioritized = false;
                status = Status.FAILURE;
                return status;
            }

            status = Status.RUNNING;
            return status;
        }
    }
}