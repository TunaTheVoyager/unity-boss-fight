using UnityEngine;

public class raiderWalkControl : StateMachineBehaviour
{
    Transform bossPosition;
    Transform raiderPosition;
    raiderWeapon raiderWeaponScript;

    [SerializeField]float attackRange = 2.3f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossPosition = GameObject.FindGameObjectWithTag("Boss").transform;
        raiderWeaponScript = animator.GetComponent<raiderWeapon>();
        raiderPosition = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //for(int i = 0; i<100; i++)
        //{
        //    animator.SetTrigger("attacking");
        //    animator.ResetTrigger("attacking");
        //    if (Vector2.Distance(bossPosition.position, raiderPosition.position) > attackRange)
        //    {
        //        animator.SetBool("walking", true);
        //        break;
        //    }
        //}
        if (Vector2.Distance(bossPosition.position, raiderPosition.position) <= attackRange)
        {
            animator.SetTrigger("attacking");
            animator.ResetTrigger("attacking");
        }
        if (Vector2.Distance(bossPosition.position, raiderPosition.position) > attackRange && raiderWeaponScript.shotgunAmmo > 0)
        {

                animator.SetBool("walking", true);

        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
