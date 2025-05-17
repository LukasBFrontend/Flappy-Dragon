using UnityEngine;

public class AnimatorVisibilityController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        bool isActive = animator.GetBool("IsCharging");
        spriteRenderer.enabled = isActive;
    }
}
