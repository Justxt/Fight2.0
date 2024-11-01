using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CharacterAnimation enemyAnim;
    private Rigidbody2D myBody;

    [SerializeField]
    private float movementSpeed;

    private Transform playerTarget;

    private bool followPlayer, attackPlayer;
    [SerializeField]
    private float attack_Distance;
    [SerializeField]
    private float chasePlayerAfterAttack;

    private float currentAttackTime;
    [SerializeField]
    private float defaultAttackTime;


    private Collider2D playerCollider;


    [SerializeField]
    private GameObject punch1AttackPoint, punch2AttackPoint, kickAttackPoint;


    private void Awake()
    {
        playerCollider = GameObject.FindGameObjectWithTag(TagManager.Tags.PlayerTag).GetComponent<Collider2D>();

        enemyAnim = GetComponent<CharacterAnimation>();
        myBody = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.FindWithTag(TagManager.Tags.PlayerTag).transform;

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCollider);
    }

    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
        currentAttackTime = defaultAttackTime;
    }

    // Update is called once per frame
    void Update()
    {
        FancingToTarget();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
        AttackPlayer();
    }

    void FollowPlayer()
    {
        if (!followPlayer) return;

        float distanceToPlayer = Mathf.Abs(transform.position.x - playerTarget.position.x);

        if (distanceToPlayer > attack_Distance)
        {
            if (transform.position.x < playerTarget.position.x)
            {
                myBody.velocity = new Vector2(movementSpeed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(-movementSpeed, myBody.velocity.y);
            }

            enemyAnim.Walk(1);

            FancingToTarget();
        }
        else
        {
            //activa el ataque
            myBody.velocity = Vector2.zero;
            enemyAnim.Walk(0);
            attackPlayer = true;
            followPlayer = false;
        }
    }


    void AttackPlayer()
    {
        if (!attackPlayer) return;

        currentAttackTime += Time.deltaTime;

        // Realiza un ataque solo si ha pasado el tiempo necesario
        if (currentAttackTime >= defaultAttackTime)
        {
            Attack(UnityEngine.Random.Range(0, 5));
            currentAttackTime = 0f;  // Reinicia el temporizador
        }

        if (Mathf.Abs(transform.position.x - playerTarget.position.x) > attack_Distance + chasePlayerAfterAttack)
        {
            followPlayer = true;
            attackPlayer = false;
        }
    }


    void FancingToTarget()
    {
        if (playerTarget.position.x < transform.position.x)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
        }
        else
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
        }
    }

    void Attack(int i)
    {
        switch (i)
        {
            case 0:
                enemyAnim.Punch();
                break;
            case 1:
                enemyAnim.Punch();
                enemyAnim.Punch2();
                break;
            case 2:
                enemyAnim.Punch();
                enemyAnim.Punch2();
                enemyAnim.Kick();
                break;
            case 3:
                enemyAnim.Kick();
                break;
        }
    }

    public void ActivatePunch1()
    {
        punch1AttackPoint.SetActive(true);
    }

    public void ActivatePunch2()
    {
        punch2AttackPoint.SetActive(true);
    }

    public void ActivateKick()
    {
        kickAttackPoint.SetActive(true);
    }

    public void DeactivatePunch1()
    {
        punch1AttackPoint.SetActive(false);
    }

    public void DeactivatePunch2()
    {
        punch2AttackPoint.SetActive(false);
    }

    public void DeactivateKick()
    {
        kickAttackPoint.SetActive(false);
    }

    public void DeactivateAllAttack()
    {
        punch1AttackPoint.SetActive(false);
        punch2AttackPoint.SetActive(false);
        kickAttackPoint.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PunchAttack"))
        {
            enemyAnim.Hurt();
        }

        if (collision.gameObject.CompareTag("KickAttack"))
        {
            enemyAnim.Hurt();
        }
    }
}