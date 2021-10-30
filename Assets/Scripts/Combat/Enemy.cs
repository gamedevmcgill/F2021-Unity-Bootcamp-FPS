using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public Transform PlayerTransform;

    public NavMeshAgent NavAgent;

    private void Start() {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = playerObject.transform;
    }
}
