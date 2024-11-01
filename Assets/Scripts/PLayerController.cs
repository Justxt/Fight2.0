using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float forceJump;

    private bool facingRight;
    private bool isGrounded;
    private bool isJumping;

    private CharacterAnimation anim;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<CharacterAnimation>();
    }

    private void Start() {
        facingRight = true;
    }

    private void Update() {
        checkInputUser();
    }

    private void FixedUpdate() {
        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);
        Flip(horizontal);
    }

    private void HandleMovement(float horizontal) {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        anim.Walk(horizontal);
    }

    private void Flip(float horizontal) {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void checkInputUser() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            isJumping = true;
            anim.Jump(true);
            rb.AddForce(new Vector2(0, forceJump), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            anim.Jump(false);
            isGrounded = true;
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("PunchAttack")) {
            anim.Hurt();
        }

        if (collision.gameObject.CompareTag("KickAttack")) {
            anim.Hurt();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }


}