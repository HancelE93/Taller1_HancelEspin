
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    private Rigidbody2D rd;
    private Animator animator;
    private PlayerHealth playerHealth;

    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private bool facinRight = true;
    private bool isGrounded;

    private bool isKnockedBack = false;
    private float knockbackTimer = 0.25f;
    private float knockbackDuration = 0.25f;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Mientras ocurre el empujón, no aceptar controles
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;

            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
            }
            else
            {
                animator.SetFloat("speed", 0f);
                return;
            }
        }

        float move = Input.GetAxis("Horizontal");

        float speedAnimation = Mathf.Abs(move);
        animator.SetFloat("speed", speedAnimation);

        rd.linearVelocity = new Vector2(
            move * moveSpeed,
            rd.linearVelocity.y
        );

        // Girar al personaje
        if (move > 0 && !facinRight)
        {
            Flip();
        }
        else if (move < 0 && facinRight)
        {
            Flip();
        }

        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rd.linearVelocity = new Vector2(rd.linearVelocity.x, 0f);

            rd.AddForce(
                Vector2.up * jumpForce,
                ForceMode2D.Impulse
            );

            isGrounded = false;
            animator.SetBool("isJump", true);
        }
    }

    public void StartKnockback()
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJump", false);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(collision.gameObject);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Flip()
    {
        facinRight = !facinRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }
}
