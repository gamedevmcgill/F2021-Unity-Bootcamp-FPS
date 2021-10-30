using UnityEngine;

public class Weapon : MonoBehaviour {
    public Transform EyeTransform;

    public float ShotDamage = 10;

    public void Shoot() {
        Vector3 EyePosition = EyeTransform.position;
        Vector3 EyeDirection = EyeTransform.forward;

        Debug.DrawRay(EyePosition, EyeDirection * 100, Color.red, 5.0f);

        RaycastHit hit;
        if (Physics.Raycast(EyePosition, EyeDirection, out hit)) {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("hit object " + hitObject.name);
        }
    }
}
