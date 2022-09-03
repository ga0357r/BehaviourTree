using System.Collections.Generic;

public class Node
{
    protected string name;
    protected Status status;
    protected List<Node> children =  new List<Node>();
    public int CurrentChild { get; set; }

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

    public virtual Status Evaluate()
    {
        return Status.SUCCESS;
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
}
