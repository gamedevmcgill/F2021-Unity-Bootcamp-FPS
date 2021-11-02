using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    public Transform EyeTransform;

    public GameObject MuzzleFlashPrefab;
    public Transform MuzzleMarker;

    public float ShotDamage = 10;

    public void Shoot() {
        Vector3 EyePosition = EyeTransform.position;
        Vector3 EyeDirection = EyeTransform.forward;

        Debug.DrawRay(EyePosition, EyeDirection * 100, Color.red, 5.0f);

        RaycastHit hit;
        if (Physics.Raycast(EyePosition, EyeDirection, out hit)) {
            GameObject hitObject = hit.collider.gameObject;
            HealthComponent healthComponent = hitObject.GetComponent<HealthComponent>();
            if (healthComponent) {
                healthComponent.TakeDamage(ShotDamage);
            }

            Debug.Log("hit object " + hitObject.name);
        }

        Instantiate(MuzzleFlashPrefab, MuzzleMarker);
    }
}
