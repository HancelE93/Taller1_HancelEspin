
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

    [Header("Sonido de muerte")]
    public AudioSource audioSource;
    public AudioClip deathSound;

    [Header("Sonido al recibir un golpe")]
    public AudioClip hitSound;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log("Vida del enemigo: " + currentHealth);

        // Sonido de impacto solo si sobrevive al golpe.
        if (currentHealth > 0 &&
            audioSource != null &&
            hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
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

        EnemyPatrol patrol = GetComponent<EnemyPatrol>();

        if (patrol != null)
        {
            patrol.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        Collider2D[] colliders =
            GetComponentsInChildren<Collider2D>();

        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        StartCoroutine(DestroyAfterDeath());
    }

    // Lo llama el Animation Event del clip DeadEnemy.
    public void PlayDeathSound()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(deathAnimationDuration);

        Destroy(gameObject);
    }
}
