using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public Transform PlayerTransform;

    public NavMeshAgent NavAgent;

    // create a member variable to keep track of the instructed destination
    private Vector3 InstructedDestination;

    // create a member variable to keep track of the current patrolling state
    private bool InstructedDestSet;

    // create a member variable that specifies instructed destination generation range
    public float InstructedDestinationGenerationRange; // distance from player

    // create a member variable that specifies instructed destination proximity
    // (how close to an instructed destination is considered "reaching the instructed destination")
    public float ProximityRange;

    // create a member variable that specifies the LayerMask for ground to help check if desintaion is on the map
    public LayerMask GroundMask;

    private void Start() {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = playerObject.transform;
    }

    private void FixedUpdate() {
        // check to see if enemy is near the player
        if (Vector3.Distance(transform.position, PlayerTransform.position) > ProximityRange) {
            // if it is not, keep patrolling
            Patrol();
        }
    }

    private void Patrol() {
        // check to see if there is an instructed destination assigned
        // if it is not assigned, assign a new one
        if (!InstructedDestSet) {
            // randomly generate a point on the map around the player
            InstructedDestination =
                new Vector3(
                    PlayerTransform.position.x + Random.Range(-InstructedDestinationGenerationRange, InstructedDestinationGenerationRange),
                    transform.position.y,
                    PlayerTransform.position.z + Random.Range(-InstructedDestinationGenerationRange, InstructedDestinationGenerationRange));

            // ensure that the point is on the map
            if (Physics.Raycast(InstructedDestination, -transform.up, 10.0f, GroundMask)) {
                InstructedDestSet = true;
                NavAgent.SetDestination(InstructedDestination);
            } else {
                // if it is not, do nothing. Another attempt will be made in the next FixedUpdate()
                return;
            }

            
        }

        // check if the enemy has reached an instructed destination (within a certain distance from the instructed destination)
        if (Vector3.Distance(InstructedDestination, transform.position) < ProximityRange) {
            InstructedDestSet = false;
        }

    }
}
