using UnityEngine;

public class Weapon : MonoBehaviour {
    public Transform EyeTransform;

    public void Shoot() {
        Vector3 EyePosition = EyeTransform.position;
        Vector3 EyeDirection = EyeTransform.forward;


    }
}
