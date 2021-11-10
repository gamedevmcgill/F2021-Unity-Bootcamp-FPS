using System.Collections;
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

    public EnemyWeapon enemyWeapon;
    public float AttackDelay;
    private bool isAttacking;

    public HealthComponent healthComponent;

    public Animator animator;
    enum EnemyState
    {
        IDLE,
        FIRING,
        WALKING
    }

    private EnemyState currentState;

    //private WinLossCheck WinLossChecker; //TODO

    private void Start() {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = playerObject.transform;

        //WinLossChecker = GameObject.Find("Game State Handler").GetComponent<WinLossCheck>(); //TODO

        healthComponent.OnCharacterDied += Die;
    }

    private void Update() {
        UpdateAnimatorState();
    }

    private void FixedUpdate() {
        // check to see if enemy is near the player
        if (Vector3.Distance(transform.position, PlayerTransform.position) > ProximityRange) {
            // if it is not, keep patrolling
            Patrol();
        }
        else {
            if (!isAttacking) {
                InstructedDestSet = false;
                StartCoroutine("Attack");
            }
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
                if (NavAgent.isActiveAndEnabled == true) NavAgent.SetDestination(InstructedDestination);
                currentState = EnemyState.WALKING;
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

    private IEnumerator Attack() {
        // stop the enemy
        if (NavAgent.isActiveAndEnabled == true)  NavAgent.SetDestination(transform.position);
        isAttacking = true;
        currentState = EnemyState.FIRING;

        // look at player 
        transform.LookAt(PlayerTransform.position);

        // wait a bit
        yield return new WaitForSeconds(AttackDelay);

        // attack
        isAttacking = false;
        currentState = EnemyState.WALKING;
    }

    public void WeaponFired() {
        // WeaponFired is called by animator
        enemyWeapon.Shoot();
    }

    private void Die(GameObject g) {
        animator.SetBool("isFiring", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isIdle", false);
        animator.SetTrigger("isDead");
        healthComponent.OnCharacterDied -= Die;
        NavAgent.enabled = false;
        Destroy(this.gameObject, 5);
    }

    private void UpdateAnimatorState(){
        if (currentState == EnemyState.IDLE) {
            animator.SetBool("isFiring", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", true);
        }

        else if (currentState == EnemyState.WALKING) {
            animator.SetBool("isFiring", false);
            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
        }

        else if (currentState == EnemyState.FIRING) {
            animator.SetBool("isFiring", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", false);
        }
    }
}
