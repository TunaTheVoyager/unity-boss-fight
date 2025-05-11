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

    private void Awake()
    {
        characterRb = GetComponent<Rigidbody2D>(); //
        characterRen = GetComponent<SpriteRenderer>(); //
        characterTrailRen = GetComponent<TrailRenderer>(); //
        characterAnimator = GetComponent<Animator>(); //
        attackPointOffset = attackPoint.localPosition;
        characterCollider = GetComponent<Collider2D>(); //
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

        characterRb.linearVelocity = new Vector2(characterStats.walkSpeed * moveDirection, characterRb.linearVelocity.y);
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

            //Gizmoyu d�nd�rmek i�in.
            facingRight = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1.0f;
            characterRen.flipX = false;
            
            //Gizmoyu d�nd�rmek i�in.
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
            characterAnimator.SetBool("isAttacking",true);
        }
      
    }

    private IEnumerator Dash()
    {
        canDash = false; //Dash att�ktan sonra hemen dash atmamak i�in kilit rol�nde kullan�l�yor.
        isDashing = true; //Dash at�l�rken di�er hareketleri kilitlemek i�in kullan�yorsun.
        originalGravity = characterRb.gravityScale; // Havada dash atarken yer�ekiminden etkilenmemek i�in kullan�l�r. Burada gerekli de�il.
        characterRb.gravityScale = 0f;
        characterRb.linearVelocity = new Vector2(characterStats.dashingPower * moveDirection, characterRb.linearVelocity.y);

        characterCollider.enabled = false;
        characterAnimator.Play("bossCharacterDashAnim");

        characterTrailRen.emitting = true;
        yield return new WaitForSeconds(characterStats.dashingTime);
        StartCoroutine(cooldownsUI.wait());
        characterTrailRen.emitting = false;
        characterCollider.enabled = true;//Buraya bak!!!!
        characterRb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(characterStats.dashingCoolDown);
        canDash = true;
    }
    public void endAttack()
    {
        characterAnimator.SetBool("isAttacking", false);
    }
    public void characterAttack()
    {  
        enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemyLy);

        foreach (Collider2D colliderEnemy in enemy)
        {
            colliderEnemy.GetComponent<health>().takeDamage(damage);
        }
    }
    private void OnDrawGizmos()
    {
        //buraya yine bak!
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    void LateUpdate()
    {
        // AttackPoint�i her frame, do�ru tarafa ta��
        float x = Mathf.Abs(attackPointOffset.x) * (facingRight ? 1f : -1f);
        attackPoint.localPosition = new Vector3(
            x,
            attackPointOffset.y,
            attackPointOffset.z
        );
    }

}
