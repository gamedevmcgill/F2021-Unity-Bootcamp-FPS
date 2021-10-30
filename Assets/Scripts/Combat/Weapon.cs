using UnityEngine;

public class Weapon : MonoBehaviour {
    public Transform EyeTransform;

    public void Shoot() {
        Vector3 EyePosition = EyeTransform.position;
        Vector3 EyeDirection = EyeTransform.forward;

        RaycastHit hit;
        if (Physics.Raycast(EyePosition, EyeDirection, out hit)) {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("hit object " + hitObject.name);
        }
    }
}
