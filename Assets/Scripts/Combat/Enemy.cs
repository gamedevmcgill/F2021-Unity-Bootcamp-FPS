using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public Transform PlayerTransform;

    public NavMeshAgent NavAgent;

    // create a member variable to keep track of the instructed destination

    // create a member variable to keep track of the current patrolling state

    // create a member variable that specifies instructed destination generation range

    // create a member variable that specifies instructed destination proximity
    // (how close to an instructed destination is considered "reaching the instructed destination"


    private void Start() {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = playerObject.transform;
    }

    private void FixedUpdate() {
        // check to see if enemy is near the player


            // if it is not, keep patrolling

    }

    private void Patrol() {
        // check to see if there is an instructed destination assigned
        // if it is, do nothing


        // if it is not assigned, assign a new one


            // randomly generate a point on the map


            // ensure that the point is on the map


            // if it is not, do nothing. Another attempt will be made in the next FixedUpdate()


        // check if the enemy has reached an instructed destination (within a certain distance from the instructed destination)
    

    }
}
