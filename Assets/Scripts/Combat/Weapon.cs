using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    public Transform EyeTransform;

    public GameObject MuzzleFlashPrefab;
    public Transform MuzzleMarker;

    public int ClipSize = 30;
    private int ClipRemaining;

    public float ShotDamage = 10;

    private void Start() {
        ClipRemaining = ClipSize;
    }

    public void Shoot() {
        if (ClipRemaining <= 0) {
            Debug.Log("zero bullet in weapon clip");
            return;
        }

        ClipRemaining--;

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

    public IEnumerator Reload() {

    }
}
