
using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [Header("Salud del Enemigo")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Animación de muerte")]
    public Animator animator;
    public float deathAnimationDuration = 1.5f;

    [Header("Tipo de enemigo")]
    [Range(1, 2)]
    public int tipoEnemigo = 1;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log("Vida del enemigo: " + currentHealth);

        // Sonido de impacto si el enemigo sobrevive
        if (currentHealth > 0 && AudioManager.Instance != null)
        {
            AudioManager.Instance.ReproducirSfx(
                AudioManager.Instance.sonidoGolpe
            );
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        // Detener el movimiento del enemigo
        EnemyPatrol patrol = GetComponent<EnemyPatrol>();

        if (patrol != null)
        {
            patrol.enabled = false;
        }

        // Detener la física
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // Desactivar las colisiones
        Collider2D[] colliders =
            GetComponentsInChildren<Collider2D>();

        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        // Reproducir el sonido según el tipo de enemigo
        if (AudioManager.Instance != null)
        {
            if (tipoEnemigo == 1)
            {
                AudioManager.Instance.ReproducirSfx(
                    AudioManager.Instance.sonidoMuerteEnemigo1
                );
            }
            else if (tipoEnemigo == 2)
            {
                AudioManager.Instance.ReproducirSfx(
                    AudioManager.Instance.sonidoMuerteEnemigo2
                );
            }
        }

        // Reproducir la animación de muerte
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        StartCoroutine(DestroyAfterDeath());
    }

    // Se conserva para evitar perder el Animation Event existente.
    // El sonido ya se reproduce en Die(), así que no se repite aquí.
    public void PlayDeathSound()
    {
    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(deathAnimationDuration);

        Destroy(gameObject);
    }
}
