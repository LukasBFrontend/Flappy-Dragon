using System.Reflection;
using UnityEngine;

public class BossMissile : MonoBehaviour
{
    [Range(1f, 20f)][SerializeField] private float flightSpeed = 5f;
    [SerializeField] private float turnRateDegreesPerSecond = 20f;
    [SerializeField] private LayerMask layersToHit;
    private GameObject player;
    private Vector3 targetPosition;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private float targetTimer = .5f;
    private bool locked = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        if (player == null)
        {
            DestroyImmediate(gameObject);
            return;
        }


        rigidbody.linearVelocity = Vector2.left * flightSpeed / 2;
    }

    void Update()
    {
        /*
        targetTimer -= Time.deltaTime;

        if (!locked && targetTimer <= 0)
        {
            targetPosition = player.transform.position;

            targetPosition.y = targetPosition.y + (transform.position.y - targetPosition.y) * .5f;

            rigidbody.linearVelocity = (targetPosition - transform.position).normalized * flightSpeed;

            locked = true;

            Vector2 toTarget = (targetPosition - transform.position).normalized;

            float angle = Vector2.Angle(Vector2.left, toTarget); // Always positive

            bool isDownward = toTarget.y < 0;

            if (isDownward && angle <= 80f && angle >= 20f)
            {
                // Do something if the angle is at least 40° downward from the left vector
                Debug.Log("Target is downward at a steep angle: " + angle);
                animator.SetBool("IsSpinningDown", true);
            }
            if (!isDownward && angle <= 80f && angle >= 20f)
            {
                animator.SetBool("IsSpinningUp", true);
            }
        }
        */

        targetPosition = player.transform.position;

        RaycastHit2D up = Physics2D.Raycast(transform.position, new Vector2(-1f, 1.5f), 50f, layersToHit);
        RaycastHit2D down = Physics2D.Raycast(transform.position, new Vector2(-1f, -1.5f), 50f, layersToHit);
        if (!locked)
        {
            if (up.collider != null)
            {
                rigidbody.linearVelocity = new Vector2(-1f, 1f).normalized * flightSpeed;
                animator.SetBool("IsSpinningUp", true);
                locked = true;
            }
            if (down.collider != null)
            {
                rigidbody.linearVelocity = new Vector2(-1f, -1f).normalized * flightSpeed;
                animator.SetBool("IsSpinningDown", true);
                locked = true;
            }
        }

    }
}
