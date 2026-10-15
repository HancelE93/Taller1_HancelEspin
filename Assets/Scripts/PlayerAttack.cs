
using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [Header("Punto de ataque")]
    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask enemyLayer;

    [Header("Daño de los ataques")]
    public int damageAttack1 = 1;
    public int damageAttack2 = 2;
    public int damageAttack3 = 3;

    [Header("Animator")]
    public Animator animator;

    [Header("Sonido de espada")]
    public AudioSource audioSource;
    public AudioClip swordSound;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Ataque 1: tecla Z
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack(1, damageAttack1);
        }

        // Ataque 2: tecla X
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack(2, damageAttack2);
        }

        // Ataque 3: tecla C
        if (Input.GetKeyDown(KeyCode.C))
        {
            Attack(3, damageAttack3);
        }
    }

    void Attack(int attackNumber, int damage)
    {
        // Reproducir sonido de espada
        if (audioSource != null && swordSound != null)
        {
            audioSource.PlayOneShot(swordSound);
        }

        // Reproducir la animación del ataque
        if (animator != null)
        {
            animator.SetTrigger("Attack" + attackNumber);
        }

        // Comprobar que el punto de ataque esté asignado
        if (attackPoint == null)
        {
            Debug.LogWarning(
                "Asigna el Attack Point en el Inspector."
            );
            return;
        }

        // Detectar enemigos dentro del rango
        Collider2D[] hitEnemies =
            Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                enemyLayer
            );

        // Evitar golpear varias veces al mismo enemigo
        HashSet<EnemyHealth> enemiesHit =
            new HashSet<EnemyHealth>();

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth =
                enemy.GetComponentInParent<EnemyHealth>();

            if (enemyHealth != null &&
                enemiesHit.Add(enemyHealth))
            {
                enemyHealth.TakeDamage(damage);

                Debug.Log(
                    "Ataque " + attackNumber +
                    " | Daño: " + damage
                );
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRange
        );
    }
}
