using UnityEngine;

public class Crab : MonoBehaviour
{
    [SerializeField] private GameObject homingMissilePrefab, firePoint;
    [SerializeField] private float walkDistance = 2;
    [SerializeField] private float speed = 1f;
    [Range(.1f, 2f)][SerializeField] private float countDownTimer, launchTimer = 0.8f;
    private Animator animator;
    private bool isActive, missileFired = false;
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
        if (!isActive) isActive = transform.position.x < 9.75;

        if (isActive)
        {
            if (relativePosition.x <= startPosition.x + walkDistance)
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
            }
        }
    }

    void FireMissile()
    {
        Debug.Log("Fire");
        Instantiate(homingMissilePrefab, firePoint.transform);
        missileFired = true;
    }
}
