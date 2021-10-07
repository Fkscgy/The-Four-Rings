using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoracaoQuebrado : StateMachineBehaviour
{
    HearthBossBehaviour hbb;
    Rigidbody2D rb;
    public Transform Target;
    [SerializeField] float speed;
    [SerializeField] float meleeAttackRate;
    [SerializeField] float meleeAttackRange;
    float meleeNextAttack;
    [SerializeField] float magicAttackRate;
    float magicNextAttack;
    [SerializeField]
    float minDistance;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hbb = animator.GetComponent<HearthBossBehaviour>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       hbb.Flip();
        Target = hbb.UpdateTarget();
        
        switch(Random.Range(0,2))
        {
            case 0:
                if(magicNextAttack < Time.time)
                {
                    hbb.MagicAttack();
                    magicNextAttack = Time.time + magicAttackRate;
                }
            break;
            case 1:
                if(Vector2.Distance(Target.position, rb.position) <= meleeAttackRange && meleeNextAttack <Time.time)
                {
                    hbb.MeleeAttack();
                    meleeNextAttack = Time.time + meleeAttackRate;
                }
            break;
        }
        if(Vector2.Distance(Target.position, rb.position) >= minDistance)
        {
            Vector2 targetPosition = new Vector2(Target.position.x,rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position,targetPosition,speed *Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
