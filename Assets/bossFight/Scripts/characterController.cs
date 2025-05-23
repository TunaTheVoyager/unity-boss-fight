using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class characterController : MonoBehaviour
{
    [SerializeField] private cooldownsUI cooldownsUI;
    //Stats
    public characterStats characterStats;
    //Moving
    [SerializeField] public float moveDirection;
    private Rigidbody2D characterRb;
    private SpriteRenderer characterRen;
    //Dashing
    private TrailRenderer characterTrailRen;
    private Collider2D characterCollider;
    public bool canDash = true;
    private bool isDashing;
    private float originalGravity;
    //Animations
    private Animator characterAnimator;
    //Attacking
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLy;
    [SerializeField] private float damage;
    private Collider2D[] enemy;
    private Vector3 attackPointOffset;
    private bool facingRight;
    private bool isItAttacking = false;
    //Summon
    [SerializeField] private GameObject raider;
    public bool raiderSummoned = false;
    aggroSystem aggroSystem;

    private void Awake()
    {
        characterRb = GetComponent<Rigidbody2D>(); //
        characterRen = GetComponent<SpriteRenderer>(); //
        characterTrailRen = GetComponent<TrailRenderer>(); //
        characterAnimator = GetComponent<Animator>(); //
        attackPointOffset = attackPoint.localPosition;
        characterCollider = GetComponent<Collider2D>(); //
        aggroSystem = GetComponent<aggroSystem>();
    }
    void Start()
    {

    }

    private void FixedUpdate()
    {
        //Dash
        if (isDashing)
        {
            return;
        }
        if(isItAttacking)
        {
            // StartCoroutine("cooling");
            characterRb.linearVelocity = Vector2.zero;
        }
        else if(health.takeDamageAnim)
        {
            characterRb.linearVelocity = Vector2.zero;
        }
        else
        {
            characterRb.linearVelocity = new Vector2(characterStats.walkSpeed * moveDirection, characterRb.linearVelocity.y);
        }
    }

    void Update()
    {
        //Dash
        if (isDashing)
        {
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {

            moveDirection = -1.0f;
            characterRen.flipX = true;

            //Gizmoyu döndürmek için.
            facingRight = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1.0f;
            characterRen.flipX = false;
            
            //Gizmoyu döndürmek için.
            facingRight = true;  
        }
        else
        {
            moveDirection = 0.0f;
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            if (moveDirection != 0.0f)
            {
                StartCoroutine(Dash());
            }
        }

        //not move
        if (characterRb.linearVelocity == Vector2.zero)
        {
            characterAnimator.SetBool("isWalking", false);
        }
        else
        {
            characterAnimator.SetBool("isWalking", true);
        }

        //attack
        if (Input.GetMouseButtonDown(0))
        {
                int randomAnimationNumber = Random.Range(1, 4);
                switch (randomAnimationNumber)
                {
                    case 1:
                        attackPointOffset.x = 1.5f;
                        characterAnimator.SetInteger("attackNumber", 1);
                        characterAnimator.SetTrigger("attacking");
                        break;
                    case 2:
                        attackPointOffset.x = 1.5f;      
                        characterAnimator.SetInteger("attackNumber", 2);
                        characterAnimator.SetTrigger("attacking");
                        break;
                    case 3:
                        attackPointOffset.x = 1.2f;       
                        characterAnimator.SetInteger("attackNumber", 3);
                        characterAnimator.SetTrigger("attacking");
                        break;
                }
            isItAttacking = true;

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(raider != null)
            Instantiate(raider, transform.position,Quaternion.identity);
            raiderSummoned = true;
        }
        

    }

    private IEnumerator Dash()
    {
        canDash = false; //Dash attýktan sonra hemen dash atmamak için kilit rolünde kullanýlýyor.
        isDashing = true; //Dash atýlýrken diðer hareketleri kilitlemek için kullanýyorsun.
        originalGravity = characterRb.gravityScale; // Havada dash atarken yerçekiminden etkilenmemek için kullanýlýr. Burada gerekli deðil.
        characterRb.gravityScale = 0f;
        characterRb.linearVelocity = new Vector2(characterStats.dashingPower * moveDirection, characterRb.linearVelocity.y);

        characterCollider.enabled = false;
        //characterAnimator.Play("bossCharacterDashAnim");
        characterAnimator.SetTrigger("dash");

        characterTrailRen.emitting = true;
        yield return new WaitForSeconds(characterStats.dashingTime);
        characterCollider.enabled = true;//Buraya bak!!!!
        StartCoroutine(cooldownsUI.wait());
        characterTrailRen.emitting = false;
        characterRb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(characterStats.dashingCoolDown);
        canDash = true;
    }
    public void endAttack()
    {
        isItAttacking = false;
    }
    public void characterAttack()
    {  
        enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemyLy);

        foreach (Collider2D colliderEnemy in enemy)
        {
            colliderEnemy.GetComponent<health>().takeDamage(damage);
            aggroSystem.attackNumber++;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    void LateUpdate()
    {
        float x = Mathf.Abs(attackPointOffset.x) * (facingRight ? 1f : -1f);
        attackPoint.localPosition = new Vector3(
            x,
            attackPointOffset.y,
            attackPointOffset.z
        );
    }

    public void endTakeDamageStop()
    {
        health.takeDamageAnim = false;
    }

    //attack için

    //private IEnumerator cooling()
    //{
    //    yield return new WaitForSeconds(0.50f);
    //    isItAttacking = false;
    //}

}
