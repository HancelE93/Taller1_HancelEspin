
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vidas")]
    public int maxHealth = 3;
    private int currentHealth;

    public int CurrentHealth => currentHealth;

    [Header("Daño e invulnerabilidad")]
    public float invulnerabilityTime = 1f;
    public float knockbackForce = 8f;

    [Header("Sonido de daño")]
    public AudioSource audioSource;
    public AudioClip hurtSound;

    private bool isInvulnerable = false;

    private Rigidbody2D rb;
    private Animator animator;
    private playercontroller controller;

    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controller = GetComponent<playercontroller>();

        // Usar el Audio Source existente del jugador
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void TakeDamage(GameObject enemy)
    {
        // Evitar recibir varios golpes seguidos
        if (isInvulnerable || currentHealth <= 0)
            return;

        currentHealth--;

        // Sonido al recibir daño
        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        // Activar animación de daño
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        // Aplicar empujón alejándose del enemigo
        if (rb != null && enemy != null)
        {
            float direction =
                transform.position.x >= enemy.transform.position.x
                ? 1f : -1f;

            rb.velocity = Vector2.zero;

            rb.AddForce(
                new Vector2(direction, 0.5f) * knockbackForce,
                ForceMode2D.Impulse
            );
        }

        // Bloquear movimiento durante el empujón
        if (controller != null)
        {
            controller.StartKnockback();
        }

        // Si pierde todas las vidas, reiniciar el nivel
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );

            return;
        }

        // Activar un segundo de invulnerabilidad
        StartCoroutine(InvulnerabilityCoroutine());
    }

    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        yield return new WaitForSeconds(invulnerabilityTime);

        isInvulnerable = false;
    }
}
