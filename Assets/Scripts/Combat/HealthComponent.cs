using UnityEngine;

public class HealthComponent : MonoBehaviour {
    public float InitialHealth = 100;
    private float CurrentHealth;

    private void Start() {
        CurrentHealth = InitialHealth;
    }

    public void TakeDamage(float damageAmount) {
        CurrentHealth -= damageAmount;
        Debug.Log(gameObject.name + " took damage " + damageAmount);
    }

    public void Heal(float healAmount) {
        CurrentHealth += healAmount;
        Debug.Log(gameObject.name + " healed " + healAmount);
    }
}
