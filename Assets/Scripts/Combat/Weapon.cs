using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    public Transform EyeTransform;

    public GameObject MuzzleFlashPrefab;
    public Transform MuzzleMarker;

    public int ClipSize = 30;
    private int ClipRemaining;
    public float ReloadDelay = 5; // in seconds
    private bool isReloading;

    public float ShotDamage = 10;

    private void Start() {
        ClipRemaining = ClipSize;
    }

    public void Shoot() {
        if (isReloading) return;

        if (ClipRemaining <= 0) {
            Debug.Log("zero bullet in weapon clip");
            StartCoroutine("Reload");
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
        isReloading = true;
        Debug.Log("Reloading Weapon...");

        yield return new WaitForSeconds(ReloadDelay);
        ClipRemaining = ClipSize;
        isReloading = false;

        Debug.Log("Reloading Complete");
    }
}
