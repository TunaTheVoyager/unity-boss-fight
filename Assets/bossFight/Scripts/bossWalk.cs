using UnityEngine;

public class bossWalk : StateMachineBehaviour
{
    [Header("Components")]
    Transform playerPosition;
    Transform enemyPosition;
    SpriteRenderer enemySpriteRenderer;
    Rigidbody2D enemyRb;

    [Header("Variables")]
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float attackRange = 3f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRb = animator.GetComponent<Rigidbody2D>();
        enemyPosition = animator.GetComponent<Transform>().transform;
        enemySpriteRenderer = animator.GetComponent<SpriteRenderer>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemyPosition.position.x > playerPosition.position.x)
        {
            enemySpriteRenderer.flipX = false;
        }
        else
        {
            enemySpriteRenderer.flipX = true;
        }

        if(Vector2.Distance(playerPosition.position, enemyRb.position) <= attackRange)
        {
           //animator.SetBool("stillAttacking", true);
            enemyRb.linearVelocity = Vector2.zero;
            animator.SetTrigger("isAttacking");
        }
        else
        {
            //animator.SetBool("stillAttacking", false);
            Vector2 target = new Vector2(playerPosition.position.x, enemyRb.position.y);
            Vector2 newPosition = Vector2.MoveTowards(enemyRb.position, target, speed * Time.fixedDeltaTime);
            enemyRb.MovePosition(newPosition);
        }

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("isAttacking");
    }

}
