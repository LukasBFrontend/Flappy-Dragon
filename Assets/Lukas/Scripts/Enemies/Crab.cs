using UnityEngine;

public class Crab : MonoBehaviour
{
    [SerializeField] private GameObject homingMissilePrefab, firePoint;
    [SerializeField] private float walkDistance = 2;
    [SerializeField] private float speed = 1f;
    [Range(.1f, 2f)][SerializeField] private float countDownTimer, launchTimer = 0.8f;
    private Animator animator;
    private AnimatorClipInfo[] animatorinfo;
    private string current_animation;
    private bool isActive, missileFired, walkingBack = false;
    private Vector3 relativePosition;
    private Vector3 startPosition;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        startPosition = transform.localPosition;
        relativePosition = startPosition;
    }

    void Update()
    {
        animatorinfo = this.animator.GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;

        if (!isActive) isActive = transform.position.x < 10;

        if (isActive)
        {
            if (relativePosition.x <= startPosition.x + walkDistance && !walkingBack)
            {
                relativePosition.x += speed * Time.deltaTime;
                transform.localPosition = relativePosition;
            }
            else if (countDownTimer >= 0)
            {
                animator.SetBool("IsOpen", true);
                countDownTimer -= Time.deltaTime;
            }
            else if (!missileFired)
            {
                animator.SetBool("IsStopped", true);
                FireMissile();
            }
            else if (launchTimer >= 0)
            {
                launchTimer -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("IsOpen", false);
                if (current_animation == "RC_Walk")
                {
                    gameObject.transform.DetachChildren();
                    walkingBack = true;
                    transform.localScale = new Vector3(-1, 1, 1);
                    relativePosition.x -= speed * Time.deltaTime;
                    transform.localPosition = relativePosition;
                }
            }
        }
    }

    void FireMissile()
    {
        Instantiate(homingMissilePrefab, firePoint.transform);
        missileFired = true;
    }
}
