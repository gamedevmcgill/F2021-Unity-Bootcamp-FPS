using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Component References")]
    public HealthComponent playerHealthComponent;
    public Weapon playerWeaponComponent;

    [Header("UI References")]
    public TMP_Text playerHealthTextField;
    public TMP_Text playerAmmoTextField;

    private void Start()
    {
        playerWeaponComponent.OnWeaponStatsChanged += UpdateAmmoDisplay;
    }

    public void Update()
    {
        if (playerHealthComponent.CurrentHealth.ToString() != playerHealthTextField.text)
        {
            playerHealthTextField.text = playerHealthComponent.CurrentHealth.ToString();
        }
        if (playerWeaponComponent.isReloading == true)
        {
            playerAmmoTextField.text = "Reloading";
        }
    }

    void UpdateAmmoDisplay(int clipSize, int clipRemaining)
    {
        playerAmmoTextField.text = clipRemaining.ToString();
    }
}
