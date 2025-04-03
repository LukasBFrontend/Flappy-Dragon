using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D rigidBody;
    public float jumpForce;
    public LogicScript logic;
    public bool playerIsAlive = true;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsAlive)
        {
            rigidBody.linearVelocity = Vector2.up * jumpForce;
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
