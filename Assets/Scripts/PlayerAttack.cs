
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

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack(1, damageAttack1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack(2, damageAttack2);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Attack(3, damageAttack3);
        }
    }

    void Attack(int attackNumber, int damage)
    {
        // Sonido de espada centralizado
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ReproducirSfx(
                AudioManager.Instance.sonidoAtaque
            );
        }

        // Animación de ataque
        if (animator != null)
        {
            animator.SetTrigger("Attack" + attackNumber);
        }

        if (attackPoint == null)
        {
            Debug.LogWarning(
                "Asigna el Attack Point en el Inspector."
            );
            return;
        }

        Collider2D[] hitEnemies =
            Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                enemyLayer
            );

        // Evita golpear varias veces al mismo enemigo
        HashSet<EnemyHealth> enemiesHit =
            new HashSet<EnemyHealth>();

        // Evita aplicar daño repetido al mismo Boss
        HashSet<BossController> bossesHit =
            new HashSet<BossController>();

        foreach (Collider2D enemy in hitEnemies)
        {
            // Daño a enemigos normales
            EnemyHealth enemyHealth =
                enemy.GetComponentInParent<EnemyHealth>();

            if (enemyHealth != null &&
                enemiesHit.Add(enemyHealth))
            {
                enemyHealth.TakeDamage(damage);

                Debug.Log(
                    "Ataque " + attackNumber +
                    " | Daño al enemigo: " + damage
                );
            }

            // Daño al Boss
            BossController boss =
                enemy.GetComponentInParent<BossController>();

            if (boss != null &&
                bossesHit.Add(boss))
            {
                boss.TakeDamage(damage);

                Debug.Log(
                    "Ataque " + attackNumber +
                    " | Daño al Boss: " + damage
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
