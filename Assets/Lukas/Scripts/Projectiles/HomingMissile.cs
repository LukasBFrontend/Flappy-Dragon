using UnityEngine;
public class HomingMissile : MonoBehaviour
{
    [Range(1f, 20f)][SerializeField] private float launchSpeed, flightSpeed = 5f;
    [Range(0f, 1f)][SerializeField] private float turnTime = .5f;
    private GameObject player;
    [SerializeField] private SoundFXClip ascensionClip, flightClip;
    private Vector3 relativePosition;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private float turnTimer;
    private bool targetHeightReached, hasBegunFlight = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        relativePosition = transform.position;
        turnTimer = turnTime;

        SoundFXManager.Instance.playSoundFXClip(ascensionClip.audioClip, transform, ascensionClip.volume);
    }

    void Update()
    {
        if (player == null) return;
        if (transform.position.y <= player.transform.position.y && !targetHeightReached)
        {
            rigidbody.linearVelocityY = launchSpeed;
        }
        else if (turnTimer == turnTime)
        {
            targetHeightReached = true;
            rigidbody.linearVelocityY = 0;
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            turnTimer -= Time.deltaTime;
            animator.SetBool("IsTurning", true);
        }
        else if (turnTimer >= 0f)
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer <= turnTime / 2)
            {
                if (!hasBegunFlight) SoundFXManager.Instance.playSoundFXClip(flightClip.audioClip, transform, flightClip.volume);
                hasBegunFlight = true;
            }
        }
        else
        {
            rigidbody.linearVelocityX = -flightSpeed;
        }
    }
}
