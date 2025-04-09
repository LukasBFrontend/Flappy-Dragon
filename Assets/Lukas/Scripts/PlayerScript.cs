using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D rigidBody;
    public Animator animator; 
    public float jumpForce;
    public LogicScript logic;
    public bool playerIsAlive = true;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    // "IsJumping" is within my animation controller for the player character known as "Mechagon" Basically a on/off switch.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsAlive)
        {
            rigidBody.linearVelocity = Vector2.up * jumpForce;
            animator.SetBool("IsJumping", true);
        }
        // I made another copy of your code just to say "if player isn't touching the jump button" the animation won't play.
        if (!Input.GetKeyDown(KeyCode.Space) && playerIsAlive)
        {
           
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.GameOver();
        playerIsAlive = false;
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
