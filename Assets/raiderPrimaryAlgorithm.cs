using UnityEngine;

public class raiderPrimaryAlgorithm : StateMachineBehaviour
{
    [Header("Components")]
    Transform raiderPosition;
    Transform bossPosition;
    Rigidbody2D raiderRb;
    Vector2 target;
    Vector2 newPosition;
    raiderWeapon raiderWeapon;

    [Header("Variables")]
    [SerializeField] float raiderSpeed = 2.5f;
    [SerializeField] float raiderAttackRange = 2.3f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossPosition = GameObject.FindGameObjectWithTag("Boss").transform;
        raiderPosition = animator.GetComponent<Transform>();
        raiderRb = animator.GetComponent<Rigidbody2D>();
        raiderWeapon = animator.GetComponent<raiderWeapon>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Vector2.Distance(raiderPosition.position, bossPosition.position) <= raiderAttackRange)
        {
            animator.SetBool("walking", false);
            raiderRb.linearVelocity = Vector2.zero;
            animator.SetTrigger("attacking");
        }
        else
        {
            target = new Vector2(bossPosition.position.x, bossPosition.position.y);
            newPosition = Vector2.MoveTowards(raiderPosition.position, target, raiderSpeed * Time.fixedDeltaTime);
            raiderRb.MovePosition(newPosition);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attacking");
    }

}
