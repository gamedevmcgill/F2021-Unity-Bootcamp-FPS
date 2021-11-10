using UnityEngine;

public class HealthComponent : MonoBehaviour {
    // <access_modifier> delegate <return_type> <delegate_type_name>();
    // <access_modifier> <delegate_type> <delegate_name>;
    public delegate void OnCharacterDiedDelegate(GameObject g); //TODO
    public OnCharacterDiedDelegate OnCharacterDied;

    public float InitialHealth = 100;
    public float CurrentHealth; //TODO

    private void Start() {
        CurrentHealth = InitialHealth;
    }

    public void TakeDamage(float damageAmount) {
        CurrentHealth -= damageAmount;
        Debug.Log(gameObject.name + " took damage " + damageAmount);

        if (CurrentHealth <= 0) {

            CurrentHealth = 0; //TODO

            if (OnCharacterDied != null) {
                OnCharacterDied(gameObject); //TODO
            }
        }
    }

    public void Heal(float healAmount) {
        CurrentHealth += healAmount;
        Debug.Log(gameObject.name + " healed " + healAmount);
    }
}
