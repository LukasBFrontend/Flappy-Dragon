using UnityEngine;

public class ShieldBreak : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 1)
        {
            animator.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
