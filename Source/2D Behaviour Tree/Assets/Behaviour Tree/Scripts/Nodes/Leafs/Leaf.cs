public class Leaf : Node
{
    public delegate Status Tick();
    private Tick Action;

    public Leaf(string name, Tick Action)
    {
        this.name = name;
        this.Action = Action;
    }

    public override Status Evaluate()
    {
        if (Action != null)
        {
            return Action();
        }

        return Status.FAILURE;
    }
}
