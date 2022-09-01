using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChild : MonoBehaviour
{
    BehaviourTree tree;

    // Start is called before the first frame update
    void Start()
    {
        tree = new BehaviourTree();
        Node randomMovement = new Node("Random Movement");
        Node isPlayerSeen = new Node("Is Player Seen");
        Node isFoodSeen = new Node("Is Food Seen");
        Node moveRandomly = new Node("Move Randomly");

        randomMovement.AddChild(isPlayerSeen);
        randomMovement.AddChild(isFoodSeen);
        randomMovement.AddChild(moveRandomly);
        tree.AddChild(randomMovement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
