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
    [SerializeField] private AudioClip deathClip;
    [SerializeField][Range(0, 100)] private float audioVolume = 1f;
    [SerializeField] private float jumpForce;
    [SerializeField] private static int hearts = 3;
    [SerializeField] private GameObject shield;
    private Text heartsText;
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
        heartsText = GameObject.FindGameObjectWithTag("HeartsText").GetComponent<Text>();

        if (hearts <= 0) hearts = 3;
        heartsText.text = "Hearts: " + hearts;
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
                logic.GameOver();
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
            }
            else
            {
                LoseHeart();
            }
        }
    }

    private void LoseHeart()
    {
        animator.SetBool("IsDead", true);
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        playerIsAlive = false;
        SoundFXManager.Instance.playSoundFXClip(deathClip, transform, audioVolume);

        hearts--;
        heartsText.text = "Hearts: " + hearts;

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
