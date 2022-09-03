using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChild : MonoBehaviour
{
    private BehaviourTree tree;

    // Start is called before the first frame update
    void Start()
    {
        tree = new BehaviourTree();

        //Random Movement Branch
        Node randomMovement = new Node("Random Movement -- Sequence Node");
        Node isDifferentOrganismSeen = new Node("Is Different Organism Seen -- Leaf Node");
        Node isFoodSeen = new Node("Is Food Seen -- Leaf Node");
        Node moveRandomly = new Node("Move Randomly -- Leaf Node");
        randomMovement.AddChild(isDifferentOrganismSeen);
        randomMovement.AddChild(isFoodSeen);
        randomMovement.AddChild(moveRandomly);
        tree.AddChild(randomMovement);

        //Flee Branch
        Node flee = new Node("Flee -- Sequence Node");
        Node runAway = new Node("Run Away -- Leaf Node");
        flee.AddChild(isDifferentOrganismSeen);
        flee.AddChild(runAway);
        tree.AddChild(flee);

        tree.PrintTree();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
