
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
    }

    public void TakeDamage(GameObject enemy)
    {
        if (isInvulnerable || currentHealth <= 0)
            return;

        currentHealth--;

        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Sacudir(0.2f, 0.2f);
        }

        // Sonido de daño desde AudioManager
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ReproducirSfx(
                AudioManager.Instance.sonidoDanioJugador
            );
        }

        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        if (rb != null && enemy != null)
        {
            float direction =
                transform.position.x >= enemy.transform.position.x
                ? 1f : -1f;

            rb.linearVelocity = Vector2.zero;

            rb.AddForce(
                new Vector2(direction, 0.5f) * knockbackForce,
                ForceMode2D.Impulse
            );
        }

        if (controller != null)
        {
            controller.StartKnockback();
        }

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );

            return;
        }

        StartCoroutine(InvulnerabilityCoroutine());
    }

    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        yield return new WaitForSeconds(invulnerabilityTime);

        isInvulnerable = false;
    }
}
