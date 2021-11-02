using UnityEngine;

public class HealthComponent : MonoBehaviour {
    public float InitialHealth;
    private float CurrentHealth;

    private void Start() {
        CurrentHealth = InitialHealth;
    }

    public void TakeDamage(float damageAmount) {
        CurrentHealth -= damageAmount;
    }

    public void Heal(float healAmount) {
        CurrentHealth += healAmount;
    }
}
