using System.Collections.Generic;

public class Node
{
    public Node()
    {

    }

    public Node(string name)
    {
        this.name = name;
    }

    protected string name;
    protected Status status;
    protected List<Node> children =  new List<Node>();
    private int currentChild = 0;

    protected virtual Status Evaluate()
    {
        return status;
    }

    public void AddChild(Node child)
    {
        children.Add(child);
    }

}

public enum Status
{
    FAILURE,
    SUCCESS,
    RUNNING
}
