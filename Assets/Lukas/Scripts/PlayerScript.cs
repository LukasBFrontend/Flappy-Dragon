using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum PowerUp
{
    None,
    Blue,
    Red,
    Green
}
public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public bool playerIsAlive = true;
    [HideInInspector] public PowerUp activePowerUp = PowerUp.None;
    [HideInInspector] public bool shieldActive, flickerOn = false;
    [SerializeField] private AudioClip deathClip, respawnClip;
    [SerializeField][Range(0, 100)] private float deathClipVolume, respawnClipVolume = 1f;
    [SerializeField] private float jumpForce;
    public static int hearts = 3;
    [SerializeField] private GameObject shield;
    private HeartsManager heartsManager;
    private Rigidbody2D rigidBody;
    private Animator animator, shieldAnimator;
    private SpriteRenderer spriteRenderer, shieldSprite;
    private LogicScript logic;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        shieldAnimator = shield.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shieldSprite = shield.GetComponent<SpriteRenderer>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        heartsManager = GameObject.FindGameObjectWithTag("HeartsManager")?.GetComponent<HeartsManager>();

        if (hearts <= 0)
        {
            hearts = 3;
        }

        bool isRespawning = LogicScript.respawnPoint != new Vector2(0, 0);
        animator.SetBool("IsRespawning", isRespawning);
        if (isRespawning) SoundFXManager.Instance.playSoundFXClip(respawnClip, transform, respawnClipVolume);
    }

    void Update()
    {
        if (playerIsAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
                animator.SetBool("IsJumping", true);
            }

            if (!Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("IsJumping", false);
            }

            if (transform.position.y <= -7 || transform.position.y >= 7)
            {
                LoseHeart();
            }

            animator.SetBool("HasBlue", !flickerOn && activePowerUp == PowerUp.Blue);
            animator.SetBool("HasRed", !flickerOn && activePowerUp == PowerUp.Red);
            animator.SetBool("HasGreen", !flickerOn && activePowerUp == PowerUp.Green);
            shieldAnimator.SetBool("IsBroken", !shieldActive);
            if (shieldActive) shieldSprite.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        LoseHeart();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsHostile(collision))
        {
            if (shieldActive)
            {
                collision.gameObject.GetComponent<Enemy>()?.TakeDamage(2000);
                shieldActive = false;
                activePowerUp = PowerUp.None;
                shieldAnimator.SetBool("IsBroken", true);
                BluePower.powerUpTimer = 0;
                Debug.Log("sheild damaged");
            }
            else
            {
                LoseHeart();
            }
        }
    }

    private void LoseHeart()
    {
        if (LogicScript.Instance.isGameWon) return;
        animator.SetBool("IsDead", true);
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        playerIsAlive = false;
        SoundFXManager.Instance.playSoundFXClip(deathClip, transform, deathClipVolume);

        hearts--;
        heartsManager.ResetProgress();

        if (hearts > 0) logic.Respawn();
        else logic.GameOver();
    }

    private bool IsHostile(Collider2D collision)
    {
        string[] tagToFind = new string[] { "Ground", "Robot", "Missile" };

        for (int i = 0; i < tagToFind.Length; i++)
        {
            string testedTag = tagToFind[i];

            if (!collision.CompareTag(testedTag)) continue;

            return true;
        }

        return false;
    }
}
