using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd;
    private Animator animator;

    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private bool isGrounded;
    private bool facinRight = true;

    private float move;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Leer movimiento del teclado
        move = Input.GetAxis("Horizontal");

        // Girar personaje
        if (move > 0 && !facinRight)
        {
            Flip();
        }
        else if (move < 0 && facinRight)
        {
            Flip();
        }

        // Animación de correr
        float speedAnimation = Mathf.Abs(move);
        animator.SetFloat("speed", speedAnimation);

        // Saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            isGrounded = false;

            animator.SetBool("isJump", true);
        }
    }

    void FixedUpdate()
    {
        // Aplicar movimiento físico
        rd.linearVelocity = new Vector2(
            move * moveSpeed,
            rd.linearVelocity.y
        );
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            animator.SetBool("isJump", false);
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