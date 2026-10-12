
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Ajuste de Movimiento")]
    public float speed = 3f;
    private bool movingRight = true;

    [Header("Detectores")]
    public Transform groundCheck;
    public float distanceToGround = 1.5f;
    public float distanceToWall = 1.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal del enemigo
        rb.linearVelocity = new Vector2(
            movingRight ? speed : -speed,
            rb.linearVelocity.y
        );

        // Dirección hacia la que avanza
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;

        // Detectar si hay suelo delante
        RaycastHit2D isGroundAhead = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            distanceToGround,
            groundLayer
        );

        // Detectar si hay una pared delante
        RaycastHit2D isWallAhead = Physics2D.Raycast(
            transform.position,
            direction,
            distanceToWall,
            groundLayer
        );

        // Girar si no hay suelo o hay una pared
        if (isGroundAhead.collider == null || isWallAhead.collider != null)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {  
        Gizmos.color = Color.red;
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(
                groundCheck.position,
                Vector2.down * distanceToGround
            );
        }

        Vector2 direction = movingRight ? Vector2.right : Vector2.left;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(
            transform.position,
            direction * distanceToWall
        );
    }
}
