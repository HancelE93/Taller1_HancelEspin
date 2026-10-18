
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("Vida del Boss")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Barra de vida UI")]
    public Slider barraVidaUI;

    [Header("Movimiento")]
    public float velocidadMovimiento = 2f;
    public Transform jugador;
    public Rigidbody2D rb;

    [Header("Animaciones")]
    public Animator animator;

    [Header("Ataque del Boss")]
    public float distanciaAtaque = 2f;
    public float tiempoEntreAtaques = 2f;
    public float distanciaGolpe = 2f;
    public int dañoAtaque = 1;
    public float duracionMaximaAtaque = 1.5f;

    private float siguienteAtaque = 0f;
    private bool estaAtacando = false;
    private Coroutine seguridadAtaque;

    [Header("Fase de furia")]
    public SpriteRenderer spriteRenderer;
    public Color colorFuria = Color.red;

    [Header("Panel de Victoria")]
    public GameObject panelVictoriaUI;

    private bool enFuria = false;
    private bool estaMuerto = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (barraVidaUI != null)
        {
            barraVidaUI.maxValue = maxHealth;
            barraVidaUI.value = currentHealth;
        }

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null)
            animator = GetComponent<Animator>();

        // Ocultar el panel al comenzar la partida.
        if (panelVictoriaUI != null)
            panelVictoriaUI.SetActive(false);
        else
            Debug.LogError(
                "Asigna Panel Victoria UI en el Inspector del Boss."
            );
    }

    void Update()
    {
        if (jugador == null || rb == null || estaMuerto)
            return;

        float direccionX =
            jugador.position.x - transform.position.x;

        float distanciaX = Mathf.Abs(direccionX);

        // Orientar el boss hacia el jugador.
        if (spriteRenderer != null &&
            Mathf.Abs(direccionX) > 0.01f)
        {
            spriteRenderer.flipX = direccionX > 0;
        }

        // Si está atacando, no se mueve.
        if (estaAtacando)
            return;

        // Atacar cuando el jugador esté cerca.
        if (distanciaX <= distanciaAtaque)
        {
            if (animator != null)
                animator.SetFloat("Speed", 0f);

            if (Time.time >= siguienteAtaque)
            {
                estaAtacando = true;
                siguienteAtaque =
                    Time.time + tiempoEntreAtaques;

                if (animator != null)
                {
                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("Attack");

                    Debug.Log("EL BOSS INTENTA ATACAR");
                }

                if (seguridadAtaque != null)
                    StopCoroutine(seguridadAtaque);

                seguridadAtaque =
                    StartCoroutine(SeguridadAtaque());
            }

            return;
        }

        // Movimiento normal.
        if (animator != null)
            animator.SetFloat("Speed", 1f);

        Vector2 posicionObjetivo = new Vector2(
            jugador.position.x,
            rb.position.y
        );

        Vector2 nuevaPosicion = Vector2.MoveTowards(
            rb.position,
            posicionObjetivo,
            velocidadMovimiento * Time.deltaTime
        );

        rb.MovePosition(nuevaPosicion);
    }

    IEnumerator SeguridadAtaque()
    {
        yield return new WaitForSecondsRealtime(
            duracionMaximaAtaque
        );

        if (estaAtacando)
            TerminarAtaque();
    }

    // Animation Event: colocar en el fotograma del impacto.
    public void BossAttackHit()
    {
        if (estaMuerto || jugador == null)
            return;

        float distanciaX = Mathf.Abs(
            jugador.position.x - transform.position.x
        );

        if (distanciaX > distanciaGolpe)
            return;

        PlayerHealth vidaJugador =
            jugador.GetComponentInParent<PlayerHealth>();

        if (vidaJugador != null)
        {
            vidaJugador.TakeDamage(gameObject);
            Debug.Log("EL BOSS GOLPEÓ AL JUGADOR");
        }
        else
        {
            Debug.LogWarning(
                "No se encontró PlayerHealth en el jugador."
            );
        }
    }

    // Animation Event: colocar al final del clip Attack.
    public void TerminarAtaque()
    {
        estaAtacando = false;

        if (seguridadAtaque != null)
        {
            StopCoroutine(seguridadAtaque);
            seguridadAtaque = null;
        }
    }

    public void TakeDamage(int damage)
    {
        if (estaMuerto)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        if (barraVidaUI != null)
            barraVidaUI.value = currentHealth;

        if (!enFuria && currentHealth <= maxHealth / 2)
            ActivarFuria();

        if (currentHealth <= 0)
            Die();
    }

    void ActivarFuria()
    {
        enFuria = true;

        if (spriteRenderer != null)
            spriteRenderer.color = colorFuria;

        velocidadMovimiento *= 1.8f;
    }

    void Die()
    {
        if (estaMuerto)
            return;

        estaMuerto = true;

        // Detener el movimiento.
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        // Detener la rutina de ataque.
        if (seguridadAtaque != null)
        {
            StopCoroutine(seguridadAtaque);
            seguridadAtaque = null;
        }

        estaAtacando = false;

        // Mostrar la victoria.
        if (panelVictoriaUI == null)
        {
            Debug.LogError(
                "VICTORIA: Panel Victoria UI no está asignado."
            );
            return;
        }

        panelVictoriaUI.SetActive(true);
        panelVictoriaUI.transform.SetAsLastSibling();

        Debug.Log(
            "VICTORIA ACTIVADA: " + panelVictoriaUI.name
        );

        // Pausar el juego sin ocultar el panel.
        Time.timeScale = 0f;

        // No destruir el boss: lo dejamos detenido.
    }
}
