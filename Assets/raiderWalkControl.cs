using UnityEngine;

public class raiderWalkControl : StateMachineBehaviour
{
    Transform bossPosition;
    Transform raiderPosition;
    raiderWeapon raiderWeaponScript;
    Rigidbody2D raiderRb;

    [SerializeField]float attackRange = 2.8f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossPosition = GameObject.FindGameObjectWithTag("Boss").transform;
        raiderRb = animator.GetComponent<Rigidbody2D>();
        raiderWeaponScript = animator.GetComponent<raiderWeapon>();
        raiderPosition = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(bossPosition.position, raiderRb.position) <= attackRange)
        {
            animator.SetTrigger("attacking");
            animator.SetBool("walking",false);
            raiderRb.linearVelocity = Vector2.zero;
        }
        if (Vector2.Distance(bossPosition.position, raiderRb.position) > attackRange && raiderWeaponScript.shotgunAmmo > 0)
        {

                animator.SetBool("walking", true);

        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attacking");
    }

}
