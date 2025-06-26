using System.Reflection;
using UnityEngine;

public class BossMissile : MonoBehaviour
{
    [Range(1f, 20f)][SerializeField] private float flightSpeed = 5f;
    [SerializeField] private float turnRateDegreesPerSecond = 20f;
    [SerializeField] private LayerMask layersToHit;
    [SerializeField] private SoundFXClip ascensionClip, flightClip;
    private GameObject player;
    private Vector3 targetPosition;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private float targetTimer = .5f;
    private bool locked = false;
    private BossScript boss;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        if (player == null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        SoundFXManager.Instance.playSoundFXClip(ascensionClip.audioClip, transform, ascensionClip.volume);
        rigidbody.linearVelocity = Vector2.left * flightSpeed / 2;
    }

    void Update()
    {
        if (!boss.isMoving) rigidbody.linearVelocity = new(0, -2);
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
                SoundFXManager.Instance.playSoundFXClip(flightClip.audioClip, transform, flightClip.volume);
            }
            if (down.collider != null)
            {
                rigidbody.linearVelocity = new Vector2(-1f, -1f).normalized * flightSpeed;
                animator.SetBool("IsSpinningDown", true);
                locked = true;
                SoundFXManager.Instance.playSoundFXClip(flightClip.audioClip, transform, flightClip.volume);
            }
        }

    }
}
