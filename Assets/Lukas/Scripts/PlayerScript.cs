using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private float audioVolume = 1f;
    public Rigidbody2D rigidBody;
    public float jumpForce;

    [HideInInspector]
    public bool playerIsAlive = true;
    private Animator animator;
    private LogicScript logic;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsAlive)
        {
            rigidBody.linearVelocity = Vector2.up * jumpForce;
            animator.SetBool("IsJumping", true);
        }
        if (!Input.GetKeyDown(KeyCode.Space) && playerIsAlive)
        {
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.GameOver();
        playerIsAlive = false;
        SoundFXManager.Instance.playSoundFXClip(deathClip, transform, audioVolume);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsHostile(collision))
        {
            logic.GameOver();
            playerIsAlive = false;
        }
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
