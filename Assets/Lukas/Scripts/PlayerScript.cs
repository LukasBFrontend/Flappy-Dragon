using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public enum PowerUp
{
    None,
    Blue
}
public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public bool playerIsAlive = true;
    [HideInInspector] public PowerUp activePowerUp = PowerUp.None;
    [SerializeField] private AudioClip deathClip;
    [SerializeField][Range(0, 100)] private float audioVolume = 1f;
    [SerializeField] private float jumpForce;
    [SerializeField] private static int hearts = 3;
    private Text heartsText;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private LogicScript logic;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
                Die();
            }

            animator.SetBool("HasBlue", activePowerUp == PowerUp.Blue);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsHostile(collision))
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("IsDead", true);
        //spriteRenderer.enabled = false;
        logic.GameOver();
        playerIsAlive = false;
        hearts--;
        heartsText.text = "Hearts: " + hearts;
        if (hearts <= 0)
        {
            logic.IncrementDeathCount();
            logic.SetRespawn(new(0, 0));
        }
        SoundFXManager.Instance.playSoundFXClip(deathClip, transform, audioVolume);
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
