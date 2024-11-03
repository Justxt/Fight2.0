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
    public bool isDefending;

    public float punchDamage;
    public float kickDamage;
    public bool isDie;

    private EnemyController GetEnemy;
    private Health myHealth;

    private CharacterAnimation anim;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<CharacterAnimation>();
        myHealth = GetComponent<Health>();

    }

    private void Start()
    {
        GetEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        facingRight = true;
    }

    private void Update()
    {
        checkInputUser();
        deadCheck();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);
        Flip(horizontal);
    }

    private void HandleMovement(float horizontal)
    {
        if (isDie) return;
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        anim.Walk(horizontal);
    }

    private void Flip(float horizontal)
    {
        if (isDie) return;
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void checkInputUser()
    {
        if (isDie) return;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            anim.Jump(true);
            rb.AddForce(new Vector2(0, forceJump), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isDefending = true;
            anim.Defense(true);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            isDefending = false;
            anim.Defense(isDefending);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.Jump(false);
            isGrounded = true;
            isJumping = false;
        }

        if (isDie) return;

        if (collision.gameObject.CompareTag("PunchAttack") && !isDefending)
        {
            anim.Hurt();
            myHealth.health -= GetEnemy.punchDamage;

        }

        if (collision.gameObject.CompareTag("KickAttack") && !isDefending)
        {
            anim.Hurt();
            myHealth.health -= GetEnemy.kickDamage;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void deadCheck()
    {
        if (isDie) return;
        if (myHealth.health <= 0)
        {
            isDie = true;
            anim.Die(isDie);
        }
    }

}
