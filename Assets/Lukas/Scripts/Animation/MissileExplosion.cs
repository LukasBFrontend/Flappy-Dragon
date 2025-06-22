using UnityEngine;
using UnityEngine.UIElements;

public class MissileExplosion : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 0)
        {
            animator.gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
}
