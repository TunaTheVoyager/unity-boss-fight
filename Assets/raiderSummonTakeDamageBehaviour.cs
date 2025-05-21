using UnityEngine;

public class raiderSummonTakeDamageBehaviour : StateMachineBehaviour
{
    [Header("Components")]
    Transform raiderTransform;
    Transform bossTransform;

    [Header("Variables")]
    [SerializeField] private float attackRange = 4f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        raiderTransform = animator.GetComponent<Transform>();
        bossTransform = GameObject.FindGameObjectWithTag("Boss").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("dead") == true)
        {
              
        }
        else
        {
            if (Vector2.Distance(raiderTransform.position, bossTransform.position) <= attackRange)
                animator.SetBool("walking", true);
            else
                animator.SetTrigger("attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
