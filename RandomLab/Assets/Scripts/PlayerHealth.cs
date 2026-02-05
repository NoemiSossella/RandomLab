using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    // Variabile pubblica che indica il danno da subire (può essere modificata da altri script)
    public int TakeDamage = 10;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Metodo per applicare danno al personaggio
    public void ApplyDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"Player took {damageAmount} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died.");
        // Logica di morte (disabilitare movimento, animazioni, ecc.)
    }

    // Se il nemico ha un collider con tag "Enemy" e usa trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Usa la variabile TakeDamage per il danno
            ApplyDamage(TakeDamage);
        }
    }

    // Se usi collisioni fisiche (non trigger)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            ApplyDamage(TakeDamage);
        }
    }
}

