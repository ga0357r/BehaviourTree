using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class EnemyChild : MonoBehaviour
{
    private Root tree;
    private Path path;
    private int currentWaypoint = 0;
    [SerializeField] private float waitTime;
    [SerializeField] private float currentTime;
    [SerializeField] private int movementSpeed = 5;
    [SerializeField] private float nextWaypointDistance = 2f;
    [SerializeField] private Seeker seeker;
    [SerializeField] private List<Transform> posOfInterests;
    [SerializeField] private bool isDifferentOrganismSeen = false;
    [SerializeField] private bool isFoodSeen = false;
    [SerializeField] private Node.Status treeStatus = Node.Status.RUNNING;
    [SerializeField] private ActionState actionState;
    [SerializeField] private Camera mainCam;

    private enum ActionState
    {
        IDLE,
        BUSY
    }

    // Start is called before the first frame update
    private void Start()
    {
        waitTime = Random.Range(0.01f, 10f);
        ConstructBehaviourTree();
        StartCoroutine(RunBehaviourTree());
    }

    private void Update()
    {
        IsMainCameraCloseBy();
    }

    private void ConstructBehaviourTree()
    {
        tree = new Root();

        //Random Movement Branch
        Leaf isFoodSeen = new Leaf("Is Food Seen -- Leaf Node", IsFoodSeen);
        Inverter isFoodSeenInvert = new Inverter("Is Food Seen Invert -- Inverter Node", new List<Node> { isFoodSeen });
        Leaf isDifferentOrganismSeen = new Leaf("Is Different Organism Seen -- Leaf Node", IsDifferentOrganismSeen);
        Inverter isDifferentOrganismSeenInvert = new Inverter("Is Different Organism Seen Invert -- Inverter Node", new List<Node> { isDifferentOrganismSeen });        
        Leaf moveRandomly = new Leaf("Move Randomly -- Leaf Node", MoveRandomly);
        Sequence randomMovementSequence = new Sequence("Random Movement Sequence -- Sequence Node", new List<Node> { isDifferentOrganismSeenInvert, isFoodSeenInvert, moveRandomly });

        //Flee Branch
        Leaf flee = new Leaf("Flee -- Leaf Node", Flee);
        Sequence fleeSequence = new Sequence("Flee Sequence -- Sequence Node", new List<Node>{isDifferentOrganismSeen, flee });
        
        tree.AddChild(randomMovementSequence);
        //tree.AddChild(fleeSequence);
    }

    private Node.Status IsDifferentOrganismSeen()
    {
        if (isDifferentOrganismSeen)
        {
            return Node.Status.SUCCESS;
        }

        return Node.Status.FAILURE;
    }

    private Node.Status IsFoodSeen()
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

    private Node.Status Flee()
    {
        return SetDestination(posOfInterests[2].position);
    }

    private Node.Status MoveRandomly()
    {
        return SetDestination(ChooseRandomPointOfInterest());
    }

    private Node.Status SetDestination(Vector2 target)
    {
        float distanceToTarget = Vector2.Distance(transform.position, target);

        if (actionState == ActionState.IDLE)
        {
            seeker.StartPath(transform.position, target, OnPathComplete);
            SetState(ActionState.BUSY);
        }

        if(path == null)
        {
            Debug.Log("No existing path to target");
            return Node.Status.FAILURE;
        }

        var movementStatus = MoveToDestination(target);
        return movementStatus;
    }

    private void SetState(ActionState actionState)
    {
        this.actionState = actionState;
    }

    private Node.Status MoveToDestination(Vector2 destination)
    {
        if (path == null)
        {
            return Node.Status.FAILURE;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            path = null;
            SetState(ActionState.IDLE);
            return Node.Status.SUCCESS;
        }

        Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        float step = movementSpeed * Time.smoothDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], step);
        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance && currentWaypoint < path.vectorPath.Count) currentWaypoint++;
        return Node.Status.RUNNING;
    }

    private int GenerateRandomIndex(int arrayLength)
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, arrayLength - 1);
        return randomNumber;
    }

    private Vector2 ChooseRandomPointOfInterest()
    {
        int randomIndex = GenerateRandomIndex(posOfInterests.Count);
        return posOfInterests[randomIndex].position;
    }

    private void IsMainCameraCloseBy()
    {
        float distanceToCamera = Vector2.Distance(transform.position, mainCam.transform.position);
        

        //if camera distance is less than 10
        if (distanceToCamera < 10)
        {
            waitTime = 0.01f;
        }
        // behaviour tree updates faster

        //else 
        else
        {
            waitTime = 0.1f;
        }
        //behaviour tree updates less

        Debug.Log(distanceToCamera);
    }

    public IEnumerator RunBehaviourTree()
    {
        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= waitTime)
            {
                treeStatus = tree.Evaluate();
                currentTime = 0;
            }
            
            yield return null;
        }
    }
}