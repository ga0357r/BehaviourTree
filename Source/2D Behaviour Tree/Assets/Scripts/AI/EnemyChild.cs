using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChild : MonoBehaviour
{
    private BehaviourTree tree;
    private Path path;
    private int currentWaypoint = 0;
    [SerializeField] private int movementSpeed = 5;
    [SerializeField] private float nextWaypointDistance = 2f;
    [SerializeField] private Seeker seeker;
    [SerializeField] private List<Transform> posOfInterests;
    [SerializeField] private bool isDifferentOrganismSeen = true;
    [SerializeField] private Node.Status treeStatus = Node.Status.RUNNING;

    // Start is called before the first frame update
    private void Start()
    {
        ConstructBehaviourTree();
        seeker.StartPath(transform.position, posOfInterests[2].position, OnPathComplete);
        tree.PrintTree();
        

    }

    private void Update()
    {
        if (treeStatus != Node.Status.SUCCESS)
        {
            treeStatus = tree.Evaluate();
        }
        
    }

    private void ConstructBehaviourTree()
    {
        tree = new BehaviourTree();

        //Flee Branch
        Leaf isDifferentOrganismSeen = new Leaf("Is Different Organism Seen -- Leaf Node", IsDifferentOrganismSeen);
        Leaf runAway = new Leaf("Run Away -- Leaf Node", Move);
        Sequence flee = new Sequence("Flee -- Sequence Node", new List<Node>{ isDifferentOrganismSeen, runAway });
        tree.AddChild(flee);
    }

    private Node.Status IsDifferentOrganismSeen()
    {
        if (isDifferentOrganismSeen)
        {
            return Node.Status.SUCCESS;
        }

        return Node.Status.FAILURE;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private Node.Status Move()
    {
        if (path == null)
        {
            return Node.Status.FAILURE;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            path = null;
            return Node.Status.SUCCESS;
        }

        Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        float step = movementSpeed * Time.smoothDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], step);
        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance && currentWaypoint < path.vectorPath.Count) currentWaypoint++;
        return Node.Status.RUNNING;
    }
}